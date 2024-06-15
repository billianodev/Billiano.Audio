using NAudio.Wave;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
public interface IFireForgetSource
{
    /// <summary>
    /// 
    /// </summary>
    WaveSampleBuffer Buffer { get; }
    
    /// <summary>
    /// 
    /// </summary>
    WaveFormat WaveFormat { get; }
}