using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio.FireForget;

public static class FireForgetPlayerExtensions
{
    public static ISampleProvider Play(this FireForgetPlayer player, ISampleProvider provider, float volume)
    {
        return player.Play(new VolumeSampleProvider(provider)
        {
            Volume = volume
        });
    }

    public static ISampleProvider Play(this FireForgetPlayer player, WaveCache cache)
    {
        return player.Play(cache.ToSampleProvider());
    }

    public static ISampleProvider Play(this FireForgetPlayer player, WaveCache cache, float volume)
    {
        return player.Play(cache.ToSampleProvider(), volume);
    }
}