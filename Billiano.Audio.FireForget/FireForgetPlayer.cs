using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio.FireForget;

public sealed class FireForgetPlayer : IDisposable
{
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
    
    public int Play(IFireForgetSource source)
    {
        _mixer.AddMixerInput(source.ToSampleProvider());
        return source.Buffer.ByteBufferLength / (source.WaveFormat.AverageBytesPerSecond / 1000) + 300;
    }
    
    public void Dispose()
    {
        _player.Dispose();
    }
}