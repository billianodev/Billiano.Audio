using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NAudio.Wave;

namespace Billiano.Audio;

/// <summary>
/// 
/// </summary>
public class CodecFactory
{
    /// <summary>
    /// 
    /// </summary>
    public CodecProvider? FallbackCodec { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<string> SupportedFileExtensions => _entries.Keys;
    
    private readonly Dictionary<string, List<CodecProvider>> _entries;

    /// <summary>
    /// 
    /// </summary>
    public CodecFactory()
    {
        _entries = new Dictionary<string, List<CodecProvider>>(StringComparer.OrdinalIgnoreCase);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static CodecFactory CreateDefault()
    {
        var factory = new CodecFactory();
        factory.RegisterMp3();
        factory.RegisterWave();
        factory.RegisterVorbis();
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
        if (_entries.TryGetValue(fileExtension, out var list))
        {
            list.Add(provider);
            return;
        }
        _entries.Add(fileExtension, [provider]);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePathOrExtension"></param>
    /// <returns></returns>
    public CodecProvider GetCodecProvider(string filePathOrExtension)
    {
        var fileExtension = GetFileExtension(filePathOrExtension);
        return TryGetCodecProviderOrFallback(fileExtension)
            ?? throw new KeyNotFoundException(fileExtension);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePathOrExtension"></param>
    /// <returns></returns>
    public IReadOnlyCollection<CodecProvider> GetCodecProviders(string filePathOrExtension)
    {
        var fileExtension = GetFileExtension(filePathOrExtension);
        var providers = TryGetCodecProvidersWithFallback(fileExtension);
        
        if (providers.Count == 0)
        {
            throw new KeyNotFoundException(fileExtension);
        }

        return providers;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public WaveStream GetCodec(string filePath)
    {
        var extension = GetFileExtensionFromPath(filePath);
        var providers = TryGetCodecProvidersWithFallback(extension);
        
        if (providers.Count == 0)
        {
            throw new KeyNotFoundException(extension);
        }

        var exceptions = new Exception[providers.Count];
        for (var i = 0; i < providers.Count; i++)
        {
            try
            {
                return providers[i](filePath);
            }
            catch (Exception ex)
            {
                exceptions[i] = ex;
            }
        }

        throw new Exception("Failed to initialize all possible providers",
            new AggregateException(exceptions));
    }
    
    private List<CodecProvider> TryGetCodecProviders(string fileExtension)
    {
        return _entries.TryGetValue(fileExtension, out var result) ? result : [];
    }
    
    private List<CodecProvider> TryGetCodecProvidersWithFallback(string fileExtension)
    {   
        var providers = TryGetCodecProviders(fileExtension);
        return FallbackCodec is not null ? providers.Append(FallbackCodec).ToList() : providers;
    }

    private CodecProvider? TryGetCodecProvider(string fileExtension)
    {
        return TryGetCodecProviders(fileExtension).FirstOrDefault();
    }
    
    private CodecProvider? TryGetCodecProviderOrFallback(string fileExtension)
    {
        return TryGetCodecProvider(fileExtension) ?? FallbackCodec;
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

/// <summary>
/// 
/// </summary>
public delegate WaveStream CodecProvider(string filePath);