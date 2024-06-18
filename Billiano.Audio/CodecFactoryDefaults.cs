using NAudio.Vorbis;
using NAudio.Wave;
using NLayer.NAudioSupport;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
public static class CodecFactoryDefaults
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void RegisterWave(this CodecFactory factory)
    {
        factory.Register(["wav", "wave"],
            file => new WaveFileReader(file));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void RegisterMp3(this CodecFactory factory)
    {
        factory.Register(["mp3", "mpeg3"],
            file => new Mp3FileReaderBase(file,
                    waveFormat => new Mp3FrameDecompressor(waveFormat)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void RegisterVorbis(this CodecFactory factory)
    {
        factory.Register(["ogg"],
            file => new VorbisWaveReader(file));
    }
}