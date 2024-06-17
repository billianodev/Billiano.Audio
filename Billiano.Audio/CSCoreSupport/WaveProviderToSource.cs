using System;
using CSCore;
using NAudio.Wave;
using WaveFormat = CSCore.WaveFormat;

namespace Billiano.Audio.CSCoreSupport;

/// <summary>
/// 
/// </summary>
/// <param name="provider"></param>
public class WaveProviderToSource(IWaveProvider provider) : IWaveSource
{
    /// <summary>
    /// 
    /// </summary>
    public bool CanSeek => false;
    
    /// <summary>
    /// 
    /// </summary>
    public WaveFormat WaveFormat => provider.WaveFormat.ToCSCore();
    
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