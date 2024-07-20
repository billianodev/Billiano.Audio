using NAudio.Wave;

namespace Billiano.Audio.FireForget;

public sealed class ResamplingFireForgetPlayer(IWavePlayer player, WaveFormat waveFormat) : FireForgetPlayer(player, waveFormat)
{
    protected override ISampleProvider Preprocess(ISampleProvider provider)
    {
        return new ResamplingSampleProvider(provider, WaveFormat);
    }
}