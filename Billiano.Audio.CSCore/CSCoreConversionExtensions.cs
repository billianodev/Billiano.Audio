using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

public static class CSCoreConversionExtensions
{
    public static IWaveProvider ToWaveProvider(this IWaveSource source)
    {
        return new WaveSourceToStream(source);
    }

    public static WaveStream ToWaveStream(this IWaveSource source)
    {
        return new WaveSourceToStream(source);
    }

    public static ISampleProvider ToSampleProvider(this ISampleSource source)
    {
        return new SampleSourceToProvider(source);
    }

    public static IWaveSource ToWaveSource(this WaveStream provider)
    {
        return new WaveStreamToSource(provider);
    }
}