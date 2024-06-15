using System.Collections.Generic;
using System.IO;
using NAudio.Wave;

namespace Billiano.Audio.FireForget;

/// <summary>
/// Cache audio data
/// </summary>
public class FireForgetSource : IFireForgetSource
{
    public WaveSampleBuffer Buffer { get; }
    public WaveFormat WaveFormat { get; }

    private FireForgetSource(WaveSampleBuffer buffer, WaveFormat waveFormat)
    {
        Buffer = buffer;
        WaveFormat = waveFormat;
    }

    public static IFireForgetSource CreateFromRaw(byte[] data, WaveFormat waveFormat)
    {
        return new FireForgetSource(new WaveSampleBuffer(data), waveFormat);
    }
    
    public static IFireForgetSource CreateFromRaw(float[] data, WaveFormat waveFormat)
    {
        return new FireForgetSource(new WaveSampleBuffer(data), waveFormat);
    }
    
    public static IFireForgetSource CreateFromRawStream(Stream stream, WaveFormat waveFormat)
    {
        return CreateFromWaveProvider(new RawSourceWaveStream(stream, waveFormat));
    }

    internal static IFireForgetSource CreateFromSampleProvider(ISampleProvider provider)
    {
        var list = new List<float>();

        var buffer = new float[16384];
        int count;
        while ((count = provider.Read(buffer, 0, buffer.Length)) > 0)
        {
            list.AddRange(buffer[..count]);
        }
        
        return CreateFromRaw(list.ToArray(), provider.WaveFormat);
    }
    
    internal static IFireForgetSource CreateFromWaveProvider(IWaveProvider provider)
    {
        return CreateFromSampleProvider(provider.ToSampleProvider());
    }
}