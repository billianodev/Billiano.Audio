using CSCore;
using NAudio.Wave;
using WaveFormat = NAudio.Wave.WaveFormat;

namespace Billiano.Audio;

public class SampleSourceToProvider(ISampleSource source): ISampleProvider
{
    /// <summary>
    /// 
    /// </summary>
    public WaveFormat WaveFormat => source.WaveFormat.ToNAudio();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int Read(float[] buffer, int offset, int count)
    {
        return source.Read(buffer, offset, count);
    }
}