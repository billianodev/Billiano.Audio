using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
public abstract class FireForgetPlayerBase : IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    public WaveFormat WaveFormat => _mixer.WaveFormat;

    private readonly IDisposable _player;
    private readonly MixingSampleProvider _mixer;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="waveFormat"></param>
    protected FireForgetPlayerBase(IWavePlayer player, WaveFormat waveFormat)
    {
        _player = player;

        _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(waveFormat.SampleRate, waveFormat.Channels))
        {
            ReadFully = true
        };

        player.Init(_mixer);
        player.Play();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    protected virtual ISampleProvider Preprocess(ISampleProvider provider)
    {
        return provider;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    public ISampleProvider Play(ISampleProvider source)
    {
        source = Preprocess(source);
        _mixer.AddMixerInput(source);
        return source;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider">The sample provider returned by <see cref="Play(ISampleProvider)"/></param>
    public void Stop(ISampleProvider provider)
    {
        _mixer.RemoveMixerInput(provider);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Stop()
    {
        _mixer.RemoveAllMixerInputs();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        _player.Dispose();
    }
}