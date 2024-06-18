using NAudio.Wave;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
public class FireForgetPlayer : FireForgetPlayerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="waveFormat"></param>
    public FireForgetPlayer(IWavePlayer player, WaveFormat waveFormat) : base(player, waveFormat)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    protected override ISampleProvider Preprocess(ISampleProvider provider)
    {
        return new ResamplingSampleProvider(provider, WaveFormat);
    }
}