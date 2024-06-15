using System;
using CSCore;
using CSCore.Codecs;
using CSCore.Codecs.OGG;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
public static class CodecFactoryUtility
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="extensions"></param>
    /// <param name="factory"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void UseCustomFormat(string[] extensions, GetCodecAction factory)
    {
        if (extensions.Length == 0) throw new ArgumentNullException(nameof(extensions));
        CodecFactory.Instance.Register(extensions[0], new CodecFactoryEntry(factory, extensions));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="extension"></param>
    /// <param name="factory"></param>
    public static void UseCustomFormat(string extension, GetCodecAction factory)
    {
        UseCustomFormat([extension], factory);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="extensions"></param>
    public static void UseCustomFormat(GetCodecAction factory, params string[] extensions)
    {
        UseCustomFormat(extensions, factory);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public static void UseOgg()
    {
        UseCustomFormat(["ogg"],
            s => new OggSource(s).ToWaveSource());
    }
}