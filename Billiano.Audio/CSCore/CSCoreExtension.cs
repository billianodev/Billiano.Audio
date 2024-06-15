using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
public static class CSCoreExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static ISampleSource ToSampleSource(this ISampleProvider provider)
    {
        return new SampleProviderToSource(provider);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IWaveSource ToWaveSource(this IWaveProvider provider)
    {
        return new WaveProviderToSource(provider);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IWaveSource ToWaveSource(this WaveStream provider)
    {
        return new WaveStreamToSource(provider);
    }
}