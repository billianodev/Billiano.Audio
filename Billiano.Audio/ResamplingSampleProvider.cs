using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio;

public class ResamplingSampleProvider : ISampleProvider
{
    public WaveFormat WaveFormat => _source.WaveFormat;

    private readonly ISampleProvider _source;

    public ResamplingSampleProvider(ISampleProvider source, WaveFormat targetWaveFormat)
    {
        _source = source;

        if (_source.WaveFormat.SampleRate != targetWaveFormat.SampleRate)
        {
            _source = new WdlResamplingSampleProvider(_source, targetWaveFormat.SampleRate);
        }

        if (_source.WaveFormat.Channels != targetWaveFormat.Channels)
        {
            _source = (_source.WaveFormat.Channels, targetWaveFormat.Channels) switch
            {
                (1, 2) => new MonoToStereoSampleProvider(_source),
                (2, 1) => new StereoToMonoSampleProvider(_source),
                _ => throw new NotSupportedException()
            };
        }
    }

    public int Read(float[] buffer, int offset, int count)
    {
        return _source.Read(buffer, offset, count);
    }
}