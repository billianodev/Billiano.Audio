using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;

namespace Billiano.Audio.FireForget;

/// <summary>
/// Cache audio data
/// </summary>
public class WaveCache
{
    /// <summary>
    /// 
    /// </summary>
    public WaveSampleBuffer Buffer { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public WaveFormat WaveFormat { get; }
    
    private WaveCache(WaveSampleBuffer buffer, WaveFormat waveFormat)
    {
        Buffer = buffer;
        WaveFormat = waveFormat;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="waveFormat"></param>
    /// <returns></returns>
    public static WaveCache CreateFromRaw(byte[] data, WaveFormat waveFormat)
    {
        return new WaveCache(new WaveSampleBuffer(data), waveFormat);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="waveFormat"></param>
    /// <returns></returns>
    public static WaveCache CreateFromRaw(float[] data, WaveFormat waveFormat)
    {
        return new WaveCache(new WaveSampleBuffer(data), waveFormat);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="waveFormat"></param>
    /// <returns></returns>
    public static WaveCache CreateFromRawStream(Stream stream, WaveFormat waveFormat)
    {
        return CreateFromWaveProvider(new RawSourceWaveStream(stream, waveFormat));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static WaveCache CreateFromSampleProvider(ISampleProvider provider)
    {
        var list = new List<float>();

        var buffer = new float[16384];
        int count;
        while ((count = provider.Read(buffer, 0, buffer.Length)) > 0)
        {
            list.AddRange(buffer.AsSpan(count).ToArray());
        }
        
        return CreateFromRaw(list.ToArray(), provider.WaveFormat);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static WaveCache CreateFromWaveProvider(IWaveProvider provider)
    {
        var list = new List<byte>();

        var buffer = new byte[65536];
        int count;
        while ((count = provider.Read(buffer, 0, buffer.Length)) > 0)
        {
            list.AddRange(buffer.AsSpan(count).ToArray());
        }
        
        return CreateFromRaw(list.ToArray(), provider.WaveFormat);
    }
}