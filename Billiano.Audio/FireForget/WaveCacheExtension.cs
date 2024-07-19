using NAudio.Wave;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
public static class WaveCacheExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IWaveProvider ToWaveProvider(this WaveCache source)
    {
        return new WaveCacheProvider(source);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ISampleProvider ToSampleProvider(this WaveCache source)
    {
        return source.ToWaveProvider().ToSampleProvider();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static WaveCache ToWaveCache(this WaveStream stream)
    {
        return WaveCache.CreateFromWaveStream(stream);
    }
}