using CSCore;
using NAudio.Wave;

namespace Billiano.Audio.CSCoreSupport;

/// <summary>
/// 
/// </summary>
public static class WaveFormatConverter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="waveFormat"></param>
    /// <returns></returns>
    public static NAudio.Wave.WaveFormat ToNAudio(this CSCore.WaveFormat waveFormat)
    {
        return NAudio.Wave.WaveFormat.CreateCustomFormat(
            (WaveFormatEncoding)waveFormat.WaveFormatTag,
            waveFormat.SampleRate,
            waveFormat.Channels,
            waveFormat.BytesPerSecond,
            waveFormat.BlockAlign,
            waveFormat.BitsPerSample);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="waveFormat"></param>
    /// <returns></returns>
    public static CSCore.WaveFormat ToCSCore(this NAudio.Wave.WaveFormat waveFormat)
    {
        return new CSCore.WaveFormat(
            waveFormat.SampleRate,
            waveFormat.BitsPerSample,
            waveFormat.Channels,
            (AudioEncoding)waveFormat.Encoding,
            waveFormat.ExtraSize);
    }
}