using CSCore;
using NAudio.Wave;

namespace Billiano.Audio.CSCoreSupport;

/// <summary>
/// 
/// </summary>
public static class CSCoreSupportExtension
{
    #region CSCore to NAudio
    
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
    
    #endregion
    
    #region NAudio to CSCore
    
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
    
    #endregion
}