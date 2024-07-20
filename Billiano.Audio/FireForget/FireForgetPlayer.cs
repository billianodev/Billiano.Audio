using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio.FireForget;

public class FireForgetPlayer : IDisposable
{
    public WaveFormat WaveFormat => _mixer.WaveFormat;

    private readonly IWavePlayer _player;
    private readonly MixingSampleProvider _mixer;

    public FireForgetPlayer(IWavePlayer player, WaveFormat waveFormat)
    {
        _mixer = new MixingSampleProvider(waveFormat)
        {
            ReadFully = true
        };

        _player = player;
        _player.Init(_mixer);
    }

    public void Run()
    {
        _player.Play();
    }

    public ISampleProvider Play(ISampleProvider source)
    {
        source = Preprocess(source);
        _mixer.AddMixerInput(source);
        return source;
    }

    public void Stop(ISampleProvider provider)
    {
        _mixer.RemoveMixerInput(provider);
    }

    public void Stop()
    {
        _mixer.RemoveAllMixerInputs();
    }

    public void Dispose()
    {
        _player.Dispose();
    }

    protected virtual ISampleProvider Preprocess(ISampleProvider provider)
    {
        return provider;
    }
}