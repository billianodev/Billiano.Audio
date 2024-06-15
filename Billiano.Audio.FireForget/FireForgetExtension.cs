using NAudio.Wave;

namespace Billiano.Audio.FireForget;

public static class FireForgetExtension
{
    public static IFireForgetSource ToFireForgetSource(this ISampleProvider provider)
    {
        return FireForgetSource.CreateFromSampleProvider(provider);
    }
        
    public static IFireForgetSource ToFireForgetSource(this IWaveProvider provider)
    {
        return FireForgetSource.CreateFromWaveProvider(provider);
    }

    public static ISampleProvider ToSampleProvider(this IFireForgetSource source)
    {
        return new FireForgetSampleProvider(source);
    }
    
    public static IWaveProvider ToWaveProvider(this IFireForgetSource source)
    {
        return new FireForgetWaveProvider(source);
    }
}