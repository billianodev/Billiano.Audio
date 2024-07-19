using NAudio.Wave;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
public sealed class WaveCache
{
    /// <summary>
    /// 
    /// </summary>
    public byte[] Buffer { get; }

    /// <summary>
    /// 
    /// </summary>
    public WaveFormat WaveFormat { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="waveFormat"></param>
    public WaveCache(byte[] buffer, WaveFormat waveFormat)
    {
        Buffer = buffer;
        WaveFormat = waveFormat;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="waveFormat"></param>
    /// <returns></returns>
    public static WaveCache CreateFromRaw(byte[] buffer, WaveFormat waveFormat)
    {
        return new WaveCache(buffer, waveFormat);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static WaveCache CreateFromWaveStream(WaveStream stream)
    {
        var count = stream.Length - stream.Position;
        var buffer = new byte[count];
        stream.Read(buffer, 0, buffer.Length);
        return CreateFromRaw(buffer, stream.WaveFormat);
    }
}