using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
/// <param name="source"></param>
public sealed class SampleSourceToProvider(ISampleSource source) : ISampleProvider
{
    /// <summary>
    /// 
    /// </summary>
    public NAudio.Wave.WaveFormat WaveFormat => WaveFormatConverter.ToNAudio(source.WaveFormat);

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