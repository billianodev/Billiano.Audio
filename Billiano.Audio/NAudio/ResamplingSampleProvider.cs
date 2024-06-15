using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Billiano.Audio
{
    public class ResamplingSampleProvider : ISampleProvider
    {
        public WaveFormat WaveFormat => _source.WaveFormat;
        
        private readonly ISampleProvider _source;
        
        public ResamplingSampleProvider(ISampleProvider source, WaveFormat targetWaveFormat)
        {
            _source = source;
            
            if (source.WaveFormat.SampleRate != targetWaveFormat.SampleRate)
            {
                _source = new WdlResamplingSampleProvider(_source, targetWaveFormat.SampleRate);
            }
            
            if (source.WaveFormat.Channels != targetWaveFormat.Channels)
            {
                if (source.WaveFormat.Channels == 1 && targetWaveFormat.Channels == 2)
                {
                    _source = new MonoToStereoSampleProvider(_source);
                }
                else if (source.WaveFormat.Channels == 2 && targetWaveFormat.Channels == 1)
                {
                    _source = new StereoToMonoSampleProvider(_source);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            return _source.Read(buffer, offset, count);
        }
    }
}