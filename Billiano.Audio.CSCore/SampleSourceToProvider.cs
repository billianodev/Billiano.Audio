using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

public sealed class SampleSourceToProvider(ISampleSource source) : ISampleProvider
{
    public NAudio.Wave.WaveFormat WaveFormat => WaveFormatConverter.ToNAudio(source.WaveFormat);

    public int Read(float[] buffer, int offset, int count)
    {
        return source.Read(buffer, offset, count);
    }
}