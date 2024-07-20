using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

public static class WaveFormatConverter
{
    public static NAudio.Wave.WaveFormat ToNAudio(CSCore.WaveFormat waveFormat)
    {
        return NAudio.Wave.WaveFormat.CreateCustomFormat(
            (WaveFormatEncoding)waveFormat.WaveFormatTag,
            waveFormat.SampleRate,
            waveFormat.Channels,
            waveFormat.BytesPerSecond,
            waveFormat.BlockAlign,
            waveFormat.BitsPerSample);
    }

    public static CSCore.WaveFormat ToCSCore(NAudio.Wave.WaveFormat waveFormat)
    {
        return new CSCore.WaveFormat(
            waveFormat.SampleRate,
            waveFormat.BitsPerSample,
            waveFormat.Channels,
            (AudioEncoding)waveFormat.Encoding,
            waveFormat.ExtraSize);
    }
}