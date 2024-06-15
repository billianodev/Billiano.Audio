using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
public static class NAudioExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ISampleProvider ToSampleProvider(this ISampleSource source)
    {
        return new SampleSourceToProvider(source);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static WaveStream ToWaveProvider(this IWaveSource source)
    {
        return new WaveSourceToProvider(source);
    }
}