using System;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
/// <param name="source"></param>
public class FireForgetSampleProvider(IFireForgetSource source) : ISampleProvider
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
    public int Read(float[] buffer, int offset, int count)
    {
        var max = source.Buffer.FloatBufferLength - position;
        var length = Math.Min(max, count);
        Buffer.BlockCopy(source.Buffer.FloatBuffer, position * 4, buffer, offset * 4, length * 4);
        position += length;
        return length;
    }
}