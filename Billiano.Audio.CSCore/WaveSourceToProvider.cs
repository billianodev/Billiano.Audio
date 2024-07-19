using CSCore;
using NAudio.Wave;
using WaveFormat = NAudio.Wave.WaveFormat;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
/// <param name="source"></param>
public sealed class WaveSourceToProvider(IWaveSource source) : WaveStream
{
    /// <summary>
    /// 
    /// </summary>
    public override WaveFormat WaveFormat => WaveFormatConverter.ToNAudio(source.WaveFormat);

    /// <summary>
    /// 
    /// </summary>
    public override long Length => source.Length;

    /// <summary>
    /// 
    /// </summary>
    public override long Position { get => source.Position; set => source.Position = value; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
        return source.Read(buffer, offset, count);
    }
}