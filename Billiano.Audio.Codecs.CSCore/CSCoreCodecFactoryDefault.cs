using CSCore.Codecs.FLAC;
using NAudio.Wave;

namespace Billiano.Audio.Codecs.CSCore;

/// <summary>
/// 
/// </summary>
public static class CSCoreCodecFactoryDefaults
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void RegisterAIFF(this CodecFactory factory)
    {
        factory.Register(["aiff", "aif", "aifc"],
            file => new AiffFileReader(file));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void RegisterFLAC(this CodecFactory factory)
    {
        factory.Register(["flac", "fla"],
            file => new FlacFile(file));
    }
}