using CSCore;

namespace Billiano.Audio.Codecs.CSCore;

public static class CSCoreCodecFactoryExtension
{
    public static void UseFallbackCodec(this CodecFactory factory, CSCoreCodecProvider provider)
    {
        factory.FallbackCodec = file => provider(file)
            .ToWaveStream();
    }

    public static void Register(this CodecFactory factory, string[] fileExtensions, CSCoreCodecProvider provider)
    {
        factory.Register(fileExtensions,
            file => provider(file)
                .ToWaveStream());
    }
}

public delegate IWaveSource CSCoreCodecProvider(string filePath);