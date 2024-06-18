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
    /// <param name="provider"></param>
    /// <returns></returns>
    public static WaveCache ToWaveCache(this ISampleProvider provider)
    {
        return WaveCache.CreateFromSampleProvider(provider);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static WaveCache ToWaveCache(this IWaveProvider provider)
    {
        return WaveCache.CreateFromWaveProvider(provider);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ISampleProvider ToSampleProvider(this WaveCache source)
    {
        return new WaveCacheProvider(source);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IWaveProvider ToWaveProvider(this WaveCache source)
    {
        return new WaveCacheProvider(source);
    }
}