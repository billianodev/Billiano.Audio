using System;
using CSCore;
using NAudio.Wave;
using WaveFormat = CSCore.WaveFormat;

namespace Billiano.Audio;

public class SampleProviderToSource(ISampleProvider provider) : ISampleSource
{
    public bool CanSeek => false;
    public WaveFormat WaveFormat => provider.WaveFormat.ToCSCore();
    public long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
    public long Length { get => throw new NotSupportedException(); }
    
    public int Read(float[] buffer, int offset, int count)
    {
        return provider.Read(buffer, offset, count);
    }

    public void Dispose()
    {
    }
}