using System;
using CSCore;
using CSCore.SoundOut;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
public class FireForgetPlayer : IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    public NAudio.Wave.WaveFormat WaveFormat => _mixer.WaveFormat;

    private readonly IDisposable _player;
    private readonly MixingSampleProvider _mixer;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="waveFormat"></param>
    public FireForgetPlayer(IWavePlayer player, NAudio.Wave.WaveFormat waveFormat)
    {
        _player = player;
        
        _mixer = new MixingSampleProvider(NAudio.Wave.WaveFormat.CreateIeeeFloatWaveFormat(waveFormat.SampleRate, waveFormat.Channels))
        {
            ReadFully = true
        };

        player.Init(_mixer);
        player.Play();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="waveFormat"></param>
    public FireForgetPlayer(ISoundOut player, CSCore.WaveFormat waveFormat)
    {
        _player = player;

        _mixer = new MixingSampleProvider(NAudio.Wave.WaveFormat.CreateIeeeFloatWaveFormat(waveFormat.SampleRate, waveFormat.Channels));
        
        player.Initialize(_mixer.ToSampleSource().ToWaveSource());
        player.Play();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="waveFormat"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    protected virtual ISampleProvider Preprocess(ISampleProvider provider, NAudio.Wave.WaveFormat waveFormat, float volume = 1f)
    {
        provider = new VolumeSampleProvider(provider)
        {
            Volume = volume
        };
        provider = new ResamplingSampleProvider(provider, waveFormat);
        return provider;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="volume"></param>
    public void Play(ISampleProvider source, float volume = 1f)
    {
        source = Preprocess(source, _mixer.WaveFormat, volume);
        _mixer.AddMixerInput(source);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="volume"></param>
    public void Play(ISampleSource source, float volume = 1f)
    {
        Play(source.ToSampleProvider(), volume);
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