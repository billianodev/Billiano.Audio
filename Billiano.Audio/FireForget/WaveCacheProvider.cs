using System;
using NAudio.Wave;

namespace Billiano.Audio.FireForget;

public class WaveCacheProvider(WaveCache source) : IWaveProvider
{
    public WaveFormat WaveFormat => source.WaveFormat;

    private int _position;

    public int Read(byte[] buffer, int offset, int count)
    {
        var max = source.Buffer.Length - _position;
        var length = Math.Min(max, count);
        Buffer.BlockCopy(source.Buffer, _position, buffer, offset, length);
        _position += length;
        return length;
    }
}