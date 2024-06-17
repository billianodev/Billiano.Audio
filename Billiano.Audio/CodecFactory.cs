using System;
using System.Collections.Generic;
using System.IO;
using CSCore;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
public class CodecFactory
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<string> SupportedFileExtensions => _entries.Keys;
    
    private readonly Dictionary<string, CodecProvider> _entries;

    /// <summary>
    /// 
    /// </summary>
    public CodecFactory()
    {
        _entries = new Dictionary<string, CodecProvider>(StringComparer.OrdinalIgnoreCase);
    }
    
    public static CodecFactory CreateDefault()
    {
        var factory = new CodecFactory();
        CodecFactoryDefaults.RegisterAll(factory);
        return factory;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileExtensions"></param>
    /// <param name="provider"></param>
    public void Register(string[] fileExtensions, CodecProvider provider)
    {
        foreach (var fileExtension in fileExtensions)
        {
            Register(fileExtension, provider);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileExtension"></param>
    /// <param name="provider"></param>
    public void Register(string fileExtension, CodecProvider provider)
    {
        _entries.Add(fileExtension, provider);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePathOrExtension"></param>
    /// <returns></returns>
    public CodecProvider GetCodecProvider(string filePathOrExtension)
    {
        var fileExtension = GetFileExtension(filePathOrExtension);
        return GetCodecProviderInternal(fileExtension);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public IWaveSource GetCodec(string filePath)
    {
        var extension = GetFileExtension(filePath);
        var provider = GetCodecProviderInternal(extension);
        return provider(filePath);
    }
    
    private CodecProvider? TryGetCodecProviderInternal(string fileExtension)
    {
        _entries.TryGetValue(fileExtension, out var result);
        return result;
    }
    
    private CodecProvider GetCodecProviderInternal(string fileExtension)
    {
        return TryGetCodecProviderInternal(fileExtension)
            ?? throw new KeyNotFoundException(fileExtension);
    }
    
    private static string GetFileExtension(string filePathOrExtension)
    {
        return filePathOrExtension.IndexOfAny(['/', '\\', '.']) == -1
            ? filePathOrExtension
            : GetFileExtensionFromPath(filePathOrExtension);
    }

    private static string GetFileExtensionFromPath(string filePath)
    {
        return Path.GetExtension(filePath).Remove(0, 1);
    }
}

public delegate IWaveSource CodecProvider(string filePath);