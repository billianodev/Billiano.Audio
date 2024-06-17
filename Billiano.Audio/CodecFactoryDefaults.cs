using System.IO;
using Billiano.Audio.CSCoreSupport;
using CSCore;
using CSCore.Codecs.AAC;
using CSCore.Codecs.AIFF;
using CSCore.Codecs.FLAC;
using CSCore.Codecs.OGG;
using CSCore.Codecs.WAV;
using NLayer.NAudioSupport;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
public static class CodecFactoryDefaults
{
    /// <summary>
    /// Register flac, aiff and everything in <see cref="RegisterCommon"/>
    /// </summary>
    /// <param name="factory"></param>
    public static void RegisterAll(CodecFactory factory)
    {
        RegisterCommon(factory);
        RegisterFlac(factory);
        RegisterAiff(factory);
    }
    
    /// <summary>
    /// Register wav, mp3, aac and vorbis
    /// </summary>
    /// <param name="factory"></param>
    public static void RegisterCommon(CodecFactory factory)
    {
        RegisterWave(factory);
        RegisterMp3(factory);
        RegisterAac(factory);
        RegisterVorbis(factory);
    }
    
    public static void RegisterWave(CodecFactory factory)
    {
        factory.Register(["wav", "wave"],
            file => new WaveFileReader(file));
    }
    
    public static void RegisterMp3(CodecFactory factory)
    {
        factory.Register(["mp3", "mpeg3"],
            file => {
                return new NAudio.Wave.Mp3FileReaderBase(file, waveFormat => new Mp3FrameDecompressor(waveFormat))
                    .ToWaveSource();
            });
    }
    
    public static void RegisterVorbis(CodecFactory factory)
    {
        factory.Register(["ogg"],
            file => new OggSource(File.OpenRead(file))
                .ToWaveSource());
    }
    
    public static void RegisterFlac(CodecFactory factory)
    {
        factory.Register(["flac", "fla"],
            file => new FlacFile(file));
    }
    
    public static void RegisterAiff(CodecFactory factory)
    {
        factory.Register(["aiff", "aif", "aifc"],
            file => new AiffReader(file));
    }
    
    public static void RegisterAac(CodecFactory factory)
    {
        if (AacDecoder.IsSupported)
        {
            factory.Register(["aac", "adt", "adts", "m2ts", "mp2", "3g2", "3gp2", "3gp", "3gpp", "m4a", "m4v", "mp4v", "mp4", "mov"],
                file => new AacDecoder(file));
        }
    }
}