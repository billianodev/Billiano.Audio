using NAudio.Wave;

namespace Billiano.Audio.FireForget;

public sealed class WaveCache(byte[] buffer, WaveFormat waveFormat)
{
    public byte[] Buffer => buffer;

    public WaveFormat WaveFormat => waveFormat;

    public static WaveCache CreateFromRaw(byte[] buffer, WaveFormat waveFormat)
    {
        return new WaveCache(buffer, waveFormat);
    }

    public static WaveCache CreateFromWaveStream(WaveStream stream)
    {
        var buffer = stream.ReadToEnd();
        return CreateFromRaw(buffer, stream.WaveFormat);
    }
}