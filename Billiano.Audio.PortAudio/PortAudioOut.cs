using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NAudio.Wave;
using PortAudioSharp;
using Pa = PortAudioSharp.PortAudio;

namespace Billiano.Audio.PortAudio;

public sealed class PortAudioOut : IWavePlayer
{
    public float Volume
    {
        get => 1f;
        set => throw new NotSupportedException();
    }

    public WaveFormat OutputWaveFormat => _provider.WaveFormat;

    public PlaybackState PlaybackState { get; private set; }

    public event EventHandler<StoppedEventArgs> PlaybackStopped;

    // PortAudio
    private StreamParameters _streamParams;
    private Stream _stream;
    private double _suggestedLatency;
    private int? _desiredLatency;
    private uint _frameSize;

    // Audio source
    private ISampleProvider _provider;

    // Device
    private int _deviceId;
    private DeviceInfo _device;

    private bool _initialized;
    private bool _disposed;

    static PortAudioOut()
    {
        Pa.Initialize();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deviceId"></param>
    /// <param name="latency"></param>
    public PortAudioOut(int? deviceId, int? latency)
    {
        SetDevice(deviceId ?? Pa.DefaultOutputDevice);
        _desiredLatency = latency;
    }

    public PortAudioOut(int? latency) : this(null, latency)
    {
    }

    public PortAudioOut() : this(null, null)
    {
    }

    public Dictionary<int, DeviceInfo> EnumerateDevices()
    {
        var devices = Pa.DeviceCount;
        return devices < 0 ? [] : Enumerable.Range(0, devices - 1)
            .ToDictionary(x => x, Pa.GetDeviceInfo);
    }

    public void SetDevice(int id)
    {
        if (id == Pa.NoDevice)
        {
            throw new Exception("There is no device");
        }

        _deviceId = id;
        _device = Pa.GetDeviceInfo(_deviceId);
        _suggestedLatency = _device.defaultLowOutputLatency;
    }

    public void Init(IWaveProvider provider)
    {
        ThrowIfInitialized();
        ThrowIfDisposed();

        _provider = provider.ToSampleProvider();
        _frameSize = CalculateFrameSize();

        _streamParams = new StreamParameters()
        {
            channelCount = _provider.WaveFormat.Channels,
            device = _deviceId,
            sampleFormat = SampleFormat.Float32,
            suggestedLatency = _suggestedLatency
        };

        _stream = new Stream(
            null,
            _streamParams,
            _provider.WaveFormat.SampleRate,
            _frameSize,
            StreamFlags.NoFlag,
            StreamCallback,
            null);

        _stream.SetFinishedCallback(StreamFinishedCallback);
        _initialized = true;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        PlaybackState = PlaybackState.Stopped;

        try
        {
            _stream.Dispose();
        }
        catch (PortAudioException)
        {
        }

        _disposed = true;
    }

    public void Play()
    {
        ThrowIfNotInitialized();
        ThrowIfDisposed();

        PlaybackState = PlaybackState.Playing;
        _stream.Start();
    }

    public void Pause()
    {
        ThrowIfNotInitialized();
        ThrowIfDisposed();

        PlaybackState = PlaybackState.Paused;
        _stream.Stop();
    }

    public void Stop()
    {
        Dispose();
    }

    private uint CalculateFrameSize()
    {
        if (_desiredLatency is null or 0)
        {
            return Pa.FramesPerBufferUnspecified;
        }

        var bytePerSample = OutputWaveFormat.BitsPerSample / 8;
        var frameSize = OutputWaveFormat.ConvertLatencyToByteSize(_desiredLatency.Value) / bytePerSample / OutputWaveFormat.Channels;
        return (uint)frameSize;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(PortAudioOut));
        }
    }

    private void ThrowIfInitialized()
    {
        if (_initialized)
        {
            throw new InvalidOperationException("Has been initialized!");
        }
    }

    private void ThrowIfNotInitialized()
    {
        if (!_initialized)
        {
            throw new InvalidOperationException("Not initialized!");
        }
    }

    private StreamCallbackResult StreamCallback(
        IntPtr input,
        IntPtr output,
        uint frameCount,
        ref StreamCallbackTimeInfo timeInfo,
        StreamCallbackFlags statusFlags,
        IntPtr userDataPtr)
    {
        var data = new float[frameCount * _provider.WaveFormat.Channels];
        var count = _provider.Read(data, 0, data.Length);

        Marshal.Copy(data, 0, output, data.Length);

        return count == 0 ? StreamCallbackResult.Complete : StreamCallbackResult.Continue;
    }

    private void StreamFinishedCallback(IntPtr userDataPtr)
    {
        PlaybackState = PlaybackState.Stopped;
        PlaybackStopped?.Invoke(this, new StoppedEventArgs());
    }
}
