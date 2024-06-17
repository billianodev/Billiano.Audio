using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
public class ResamplingSampleProvider : ISampleProvider
{
    /// <summary>
    /// 
    /// </summary>
    public WaveFormat WaveFormat => _source.WaveFormat;
    
    private readonly ISampleProvider _source;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="targetWaveFormat"></param>
    /// <exception cref="NotSupportedException"></exception>
    public ResamplingSampleProvider(ISampleProvider source, WaveFormat targetWaveFormat)
    {
        if (source.WaveFormat.SampleRate != targetWaveFormat.SampleRate)
        {
            source = new WdlResamplingSampleProvider(source, targetWaveFormat.SampleRate);
        }
        
        if (source.WaveFormat.Channels != targetWaveFormat.Channels)
        {
            source = (source.WaveFormat.Channels, targetWaveFormat.Channels) switch
            {
                (1, 2) => new MonoToStereoSampleProvider(source),
                (2, 1) => new StereoToMonoSampleProvider(source),
                _ => throw new NotSupportedException()
            };
        }

        _source = source;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int Read(float[] buffer, int offset, int count)
    {
        return _source.Read(buffer, offset, count);
    }
}