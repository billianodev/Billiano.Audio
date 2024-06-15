using System;
using CSCore;
using NAudio.Wave;
using WaveFormat = CSCore.WaveFormat;

namespace Billiano.Audio;

public class WaveProviderToSource(WaveStream source): IWaveSource
{
    public bool CanSeek => source.CanSeek;
    public WaveFormat WaveFormat => source.WaveFormat.ToCSCore();
    public long Position { get => source.Position; set => source.Position = value; }
    public long Length => source.Length;
    
    public int Read(byte[] buffer, int offset, int count)
    {
        return source.Read(buffer, offset, count);
    }

    public void Dispose()
    {
        source.Dispose();
    }
}