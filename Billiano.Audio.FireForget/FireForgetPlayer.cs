using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio.FireForget;

public sealed class FireForgetPlayer : IDisposable
{
    public WaveFormat WaveFormat => _mixer.WaveFormat;
    
    private IWavePlayer _player;
    private MixingSampleProvider _mixer;
    
    public FireForgetPlayer(IWavePlayer player, WaveFormat waveFormat)
    {
        _player = player;
        
        _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(waveFormat.SampleRate, waveFormat.Channels))
        {
            ReadFully = true
        };

        _player.Init(_mixer);
        _player.Play();
    }
    
    public void Play(ISampleProvider source, float volume = 1f)
    {
        source = new VolumeSampleProvider(source)
        {
            Volume = volume
        };

        source = new ResamplingSampleProvider(source, _mixer.WaveFormat);
        
        _mixer.AddMixerInput(source);
    }
    
    public void Play(IFireForgetSource source, float volume = 1f)
    {
        Play(source.ToSampleProvider(), volume);
    }
    
    public void Dispose()
    {
        _player.Dispose();
    }
}