using System;
using NAudio.Wave;

namespace Billiano.Audio.FireForget;

public class FireForgetSampleProvider(IFireForgetSource source) : ISampleProvider
{
    public WaveFormat WaveFormat => source.WaveFormat;

    private int position;

    public int Read(float[] buffer, int offset, int count)
    {
        var max = source.Buffer.FloatBuffer.Length - position;
        var length = Math.Min(max, count);
        Array.Copy(source.Buffer.FloatBuffer, position, buffer, offset, length);
        position += length;
        return length;
    }
}