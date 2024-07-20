using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NAudio.Wave;

namespace Billiano.Audio;

public class CodecFactory
{
    public CodecProvider? FallbackCodec { get; set; }

    public IEnumerable<string> SupportedFileExtensions => _entries.Keys;

    private readonly Dictionary<string, List<CodecProvider>> _entries;

    public CodecFactory()
    {
        _entries = new Dictionary<string, List<CodecProvider>>(StringComparer.OrdinalIgnoreCase);
    }

    public static CodecFactory CreateDefault()
    {
        var factory = new CodecFactory();
        factory.RegisterMp3();
        factory.RegisterWave();
        factory.RegisterVorbis();
        return factory;
    }

    public void Register(string[] fileExtensions, CodecProvider provider)
    {
        foreach (var fileExtension in fileExtensions)
        {
            Register(fileExtension, provider);
        }
    }

    public void Register(string fileExtension, CodecProvider provider)
    {
        if (_entries.TryGetValue(fileExtension, out var list))
        {
            list.Add(provider);
            return;
        }

        _entries.Add(fileExtension, [provider]);
    }

    public CodecProvider GetCodecProvider(string filePathOrExtension)
    {
        var fileExtension = GetFileExtension(filePathOrExtension);
        return TryGetCodecProviderOrFallback(fileExtension)
            ?? throw new KeyNotFoundException(fileExtension);
    }

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

public delegate WaveStream CodecProvider(string filePath);