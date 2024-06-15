using System;
using NAudio.Wave;

namespace Billiano.Audio.FireForget;

public class FireForgetWaveProvider(IFireForgetSource source) : IWaveProvider
{
    public WaveFormat WaveFormat => source.WaveFormat;
    
    private int position;
    
    public int Read(byte[] buffer, int offset, int count)
    {
        var max = source.Buffer.ByteBuffer.Length - position;
        var length = Math.Min(max, count);
        Array.Copy(source.Buffer.ByteBuffer, position, buffer, offset, length);
        position += length;
        return length;
    }
}