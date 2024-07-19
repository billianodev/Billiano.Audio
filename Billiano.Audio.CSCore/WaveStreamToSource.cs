using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
/// <param name="source"></param>
public sealed class WaveStreamToSource(WaveStream source) : IWaveSource
{
    /// <summary>
    /// 
    /// </summary>
    public bool CanSeek => source.CanSeek;

    /// <summary>
    /// 
    /// </summary>
    public CSCore.WaveFormat WaveFormat => WaveFormatConverter.ToCSCore(source.WaveFormat);

    /// <summary>
    /// 
    /// </summary>
    public long Position { get => source.Position; set => source.Position = value; }

    /// <summary>
    /// 
    /// </summary>
    public long Length => source.Length;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int Read(byte[] buffer, int offset, int count)
    {
        return source.Read(buffer, offset, count);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        source.Dispose();
    }
}