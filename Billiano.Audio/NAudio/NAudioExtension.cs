using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

public static class NAudioExtension
{
    public static ISampleProvider ToSampleProvider(this ISampleSource source)
    {
        return new SampleSourceToProvider(source);
    }
    
    public static WaveStream ToWaveProvider(this IWaveSource source)
    {
        return new WaveSourceToProvider(source);
    }
}