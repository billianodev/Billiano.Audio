using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
public sealed class FireForgetPlayer : IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    public WaveFormat WaveFormat => _mixer.WaveFormat;
    
    private readonly IWavePlayer _player;
    private readonly MixingSampleProvider _mixer;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="waveFormat"></param>
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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="volume"></param>
    public void Play(ISampleProvider source, float volume = 1f)
    {
        source = new VolumeSampleProvider(source)
        {
            Volume = volume
        };

        source = new ResamplingSampleProvider(source, _mixer.WaveFormat);
        
        _mixer.AddMixerInput(source);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="volume"></param>
    public void Play(IFireForgetSource source, float volume = 1f)
    {
        Play(source.ToSampleProvider(), volume);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        _player.Dispose();
    }
}