using Billiano.Audio.CSCoreSupport;
using CSCore;
using NAudio.Wave;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
public static class FireForgetExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IFireForgetSource ToFireForgetSource(this ISampleProvider provider)
    {
        return FireForgetSource.CreateFromSampleProvider(provider);
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IFireForgetSource ToFireForgetSource(this IWaveProvider provider)
    {
        return FireForgetSource.CreateFromWaveProvider(provider);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IFireForgetSource ToFireForgetSource(this ISampleSource source)
    {
        return FireForgetSource.CreateFromSampleProvider(source.ToSampleProvider());
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IFireForgetSource ToFireForgetSource(this IWaveSource source)
    {
        return FireForgetSource.CreateFromWaveProvider(source.ToWaveProvider());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ISampleProvider ToSampleProvider(this IFireForgetSource source)
    {
        return new FireForgetSampleProvider(source);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IWaveProvider ToWaveProvider(this IFireForgetSource source)
    {
        return new FireForgetWaveProvider(source);
    }
}