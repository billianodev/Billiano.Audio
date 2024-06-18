using System;
using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
/// <param name="provider"></param>
public sealed class WaveProviderToSource(IWaveProvider provider) : IWaveSource
{
    /// <summary>
    /// 
    /// </summary>
    public bool CanSeek => false;
    
    /// <summary>
    /// 
    /// </summary>
    public CSCore.WaveFormat WaveFormat => WaveFormatConverter.ToCSCore(provider.WaveFormat);
    
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    public long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    public long Length => throw new NotSupportedException();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int Read(byte[] buffer, int offset, int count)
    {
        return provider.Read(buffer, offset, count);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
    }
}