using System;
using CSCore;
using CSCore.Codecs;
using CSCore.Codecs.OGG;

namespace Billiano.Audio;

public static class CodecFactoryUtility
{
    public static void UseCustomFormat(string[] extensions, GetCodecAction factory)
    {
        if (extensions.Length == 0) throw new ArgumentNullException(nameof(extensions));
        CodecFactory.Instance.Register(extensions[0], new CodecFactoryEntry(factory, extensions));
    }
    
    public static void UseCustomFormat(GetCodecAction factory, params string[] extensions)
    {
        UseCustomFormat(extensions, factory);
    }
    
    public static void UseOgg()
    {
        UseCustomFormat(["ogg", "ogv", "oga", "ogx", "ogm", "spx", "opus"],
            s => new OggSource(s).ToWaveSource());
    }
}