using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NAudio.Wave;
using PortAudioSharp;
using Pa = PortAudioSharp.PortAudio;

#nullable disable
namespace Billiano.Audio.PortAudio;

/// <summary>
/// 
/// </summary>
public sealed class PortAudioOut : IWavePlayer
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete("Use VolumeSampleProvider or similar", true)]
    public float Volume
    {
        get => 1f;
        set => throw new NotSupportedException();
    }

    /// <summary>
    /// 
    /// </summary>
    public WaveFormat OutputWaveFormat => _provider.WaveFormat;
    
    /// <summary>
    /// 
    /// </summary>
    public PlaybackState PlaybackState { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="latency"></param>
    public PortAudioOut(int? latency) : this(null, latency)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public PortAudioOut() : this(null, 0)
    {
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, DeviceInfo> EnumerateDevices()
    {
        var devices = Pa.DeviceCount;
        return devices < 0 ? [] : Enumerable.Range(0, devices - 1)
            .ToDictionary(x => x, Pa.GetDeviceInfo);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    public void Init(IWaveProvider provider)
    {
        CheckDisposed();

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
    }

    /// <summary>
    /// 
    /// </summary>
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
    
    /// <summary>
    /// 
    /// </summary>
    public void Play()
    {
        CheckDisposed();
        
        PlaybackState = PlaybackState.Playing;
        _stream.Start();
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void Pause()
    {
        CheckDisposed();
        
        PlaybackState = PlaybackState.Paused;
        _stream.Stop();
    }

    /// <summary>
    /// 
    /// </summary>
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
        var frameSize = OutputWaveFormat.ConvertLatencyToByteSize(_desiredLatency.Value) / bytePerSample;
        return (uint)frameSize;
    }
    
    private void CheckDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(PortAudioOut));
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
