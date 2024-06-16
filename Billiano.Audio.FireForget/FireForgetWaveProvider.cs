using System;
using NAudio.Wave;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
/// <param name="source"></param>
public class FireForgetWaveProvider(IFireForgetSource source) : IWaveProvider
{
    /// <summary>
    /// 
    /// </summary>
    public WaveFormat WaveFormat => source.WaveFormat;
    
    private int position;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int Read(byte[] buffer, int offset, int count)
    {
        var max = source.Buffer.ByteBufferLength - position;
        var length = Math.Min(max, count);
        Buffer.BlockCopy(source.Buffer.ByteBuffer, position, buffer, offset, length);
        position += length;
        return length;
    }
}