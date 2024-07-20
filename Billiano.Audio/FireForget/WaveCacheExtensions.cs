using NAudio.Wave;

namespace Billiano.Audio.FireForget;

public static class WaveCacheExtensions
{
    public static IWaveProvider ToWaveProvider(this WaveCache source)
    {
        return new WaveCacheProvider(source);
    }

    public static ISampleProvider ToSampleProvider(this WaveCache source)
    {
        return new WaveCacheProvider(source).ToSampleProvider();
    }

    public static WaveCache ToWaveCache(this WaveStream stream)
    {
        return WaveCache.CreateFromWaveStream(stream);
    }
}