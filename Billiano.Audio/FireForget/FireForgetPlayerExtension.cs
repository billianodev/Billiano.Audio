using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
public static class FireForgetPlayerExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="provider"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    public static ISampleProvider Play(this FireForgetPlayerBase player, ISampleProvider provider, float volume)
    {
        return player.Play(new VolumeSampleProvider(provider)
        {
            Volume = volume
        });
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public static ISampleProvider Play(this FireForgetPlayerBase player, WaveCache cache)
    {
        return player.Play(cache.ToSampleProvider());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="cache"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    public static ISampleProvider Play(this FireForgetPlayerBase player, WaveCache cache, float volume)
    {
        return player.Play(cache.ToSampleProvider(), volume);
    }
}