using NAudio.Vorbis;
using NAudio.Wave;
using NLayer.NAudioSupport;

namespace Billiano.Audio;

public static class CodecFactoryDefaults
{
    public static void RegisterWave(this CodecFactory factory)
    {
        factory.Register(["wav", "wave"],
            file => new WaveFileReader(file));
    }

    public static void RegisterMp3(this CodecFactory factory)
    {
        factory.Register(["mp3", "mpeg3"],
            file => new Mp3FileReaderBase(file,
                    waveFormat => new Mp3FrameDecompressor(waveFormat)));
    }

    public static void RegisterVorbis(this CodecFactory factory)
    {
        factory.Register(["ogg"],
            file => new VorbisWaveReader(file));
    }
}