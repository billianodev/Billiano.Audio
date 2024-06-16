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
    public float Volume { get; set; }
    

    /// <summary>
    /// 
    /// </summary>
    public WaveFormat OutputWaveFormat { get; private set; }
    
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
    private double? _desiredLatency;
    private double _suggestedLatency;
    private int _frameSize;
    
    // Audio source
    private ISampleProvider _provider;
    private int _sampleRate;
    private int _channels;
    
    // Device
    private int _deviceId;
    private DeviceInfo _device;

    private bool _disposed;
    
    static PortAudioOut()
    {
        Pa.Initialize();
    }

    public PortAudioOut(int? deviceId = null, int? latency = null)
    {
        Volume = 1f;
        SetDevice(deviceId ?? Pa.DefaultOutputDevice);
        _desiredLatency = latency;
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
        _deviceId = id;
        _device = Pa.GetDeviceInfo(_deviceId);
        _suggestedLatency = _device.defaultLowOutputLatency;
        OutputWaveFormat = WaveFormat.CreateIeeeFloatWaveFormat((int)_device.defaultSampleRate, _device.maxOutputChannels);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    public void Init(IWaveProvider provider)
    {
        CheckDisposed();

        _provider = provider.ToSampleProvider();
        _sampleRate = provider.WaveFormat.SampleRate;
        _channels = provider.WaveFormat.Channels;
        _frameSize = LatencyToSampleSize(_desiredLatency ?? _suggestedLatency);

        _streamParams = new StreamParameters()
        {
            channelCount = _channels,
            device = _deviceId,
            sampleFormat = SampleFormat.Float32,
            suggestedLatency = _suggestedLatency
        };
   
        _stream = new Stream(
            null,
            _streamParams,
            _sampleRate,
            (uint)_frameSize,
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
        _stream.Dispose();
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
    
    private void CheckDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(PortAudioOut));
        }
    }
    
    private int LatencyToSampleSize(double ms)
    {
        var bytesPerSecond = _provider.WaveFormat.AverageBytesPerSecond;
        var bytesPerSample = _provider.WaveFormat.BitsPerSample / 8;
        return (int)(ms * bytesPerSecond / bytesPerSample / 1000.0);
    }

    private StreamCallbackResult StreamCallback(
        IntPtr input,
        IntPtr output,
        uint frameCount,
        ref StreamCallbackTimeInfo timeInfo,
        StreamCallbackFlags statusFlags,
        IntPtr userDataPtr)
    {
        // Multiply by two or else it will sound weird (idk why and how this works lol)
        var data = new float[frameCount * 2];
        var count = _provider.Read(data, 0, data.Length);

        if (Volume == 1f)
        {
            Marshal.Copy(data, 0, output, data.Length);
        }
        else
        {
            unsafe
            {
                var buffer = (float*)output;
                foreach (var value in data)
                {
                    *buffer++ = value * Volume;
                }
            }
        }

        return count == 0 ? StreamCallbackResult.Complete : StreamCallbackResult.Continue;
    }   
    
    private void StreamFinishedCallback(IntPtr userDataPtr)
    {
        PlaybackState = PlaybackState.Stopped;
        PlaybackStopped?.Invoke(this, new StoppedEventArgs());
    }
}
