using CSCore.Codecs.FLAC;
using NAudio.Wave;

namespace Billiano.Audio.Codecs.CSCore;

public static class CSCoreCodecFactoryDefaults
{
    public static void RegisterAIFF(this CodecFactory factory)
    {
        factory.Register(["aiff", "aif", "aifc"],
            file => new AiffFileReader(file));
    }

    public static void RegisterFLAC(this CodecFactory factory)
    {
        factory.Register(["flac", "fla"],
            file => new FlacFile(file));
    }
}