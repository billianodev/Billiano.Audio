using CSCore;
using NAudio.Wave;
using WaveFormat = NAudio.Wave.WaveFormat;

namespace Billiano.Audio;

public class SampleSourceToProvider(ISampleSource source): ISampleProvider
{
    public WaveFormat WaveFormat => source.WaveFormat.ToNAudio();
    
    public int Read(float[] buffer, int offset, int count)
    {
        return source.Read(buffer, offset, count);
    }
}