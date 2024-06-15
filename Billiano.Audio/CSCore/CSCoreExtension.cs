using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

public static class CSCoreExtension
{
    public static ISampleSource ToSampleSource(this ISampleProvider provider)
    {
        return new SampleProviderToSource(provider);
    }
    
    public static IWaveSource ToWaveSource(this WaveStream provider)
    {
        return new WaveProviderToSource(provider);
    }
}