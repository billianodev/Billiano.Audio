using NAudio.Wave;

namespace Billiano.Audio.FireForget;

public interface IFireForgetSource
{
    WaveSampleBuffer Buffer { get; }
    WaveFormat WaveFormat { get; }
}