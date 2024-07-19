using CSCore;

namespace Billiano.Audio.Codecs.CSCore;

/// <summary>
/// 
/// </summary>
public static class CSCoreCodecFactoryExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="provider"></param>
    public static void UseFallbackCodec(this CodecFactory factory, CSCoreCodecProvider provider)
    {
        factory.FallbackCodec = file => provider(file)
            .ToWaveProvider();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="fileExtensions"></param>
    /// <param name="provider"></param>
    public static void Register(this CodecFactory factory, string[] fileExtensions, CSCoreCodecProvider provider)
    {
        factory.Register(fileExtensions,
            file => provider(file)
                .ToWaveProvider());
    }
}

/// <summary>
/// 
/// </summary>
/// <param name="filePath"></param>
/// <returns></returns>
public delegate IWaveSource CSCoreCodecProvider(string filePath);