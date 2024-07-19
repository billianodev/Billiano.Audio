using CSCore.Codecs.AAC;
using CSCore.Codecs.DDP;
using CSCore.Codecs.MP1;
using CSCore.Codecs.MP2;
using CSCore.Codecs.WMA;
using CSCore.MediaFoundation;

namespace Billiano.Audio.Codecs.CSCore.MediaFoundation;

/// <summary>
/// 
/// </summary>
public static class CSCoreMediaFoundationCodecFactoryDefaults
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void UseMediaFoundationFallback(this CodecFactory factory)
    {
        factory.UseFallbackCodec(file => new MediaFoundationDecoder(file));
    }

    /// <summary>
    /// 
    /// </summary>
    public static void TryRegisterAAC(this CodecFactory factory)
    {
        if (AacDecoder.IsSupported)
        {
            factory.Register(["aac", "adt", "adts", "m2ts", "mp2", "3g2", "3gp2", "3gp", "3gpp", "m4a", "m4v", "mp4v", "mp4", "mov"],
                file => new AacDecoder(file));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void TryRegisterWMA(this CodecFactory factory)
    {
        try
        {
            if (WmaDecoder.IsSupported)
            {
                factory.Register(["asf", "wm", "wmv", "wma"],
                    file => new WmaDecoder(file));
            }
        }
        catch
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void TryRegisterMp1(this CodecFactory factory)
    {
        if (Mp1Decoder.IsSupported)
        {
            factory.Register(["mp1", "m2ts"],
                file => new Mp1Decoder(file));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void TryRegisterMp2(this CodecFactory factory)
    {
        if (Mp2Decoder.IsSupported)
        {
            factory.Register(["mp2", "m2ts"],
                file => new Mp2Decoder(file));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    public static void TryRegisterDDP(this CodecFactory factory)
    {
        if (DDPDecoder.IsSupported)
        {
            factory.Register(["mp2", "m2ts", "m4a", "m4v", "mp4v", "mp4", "mov", "asf", "wm", "wmv", "wma", "avi", "ac3", "ec3"],
                file => new DDPDecoder(file));
        }
    }
}