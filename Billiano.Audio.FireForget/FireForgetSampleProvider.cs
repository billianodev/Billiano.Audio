using System;
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
        var max = source.Buffer.FloatBuffer.Length - position;
        var length = Math.Min(max, count);
        Array.Copy(source.Buffer.FloatBuffer, position, buffer, offset, length);
        position += length;
        return length;
    }
}