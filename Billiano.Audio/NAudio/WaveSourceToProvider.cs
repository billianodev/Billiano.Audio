using CSCore;
using NAudio.Wave;
using WaveFormat = NAudio.Wave.WaveFormat;

namespace Billiano.Audio;

public class WaveSourceToProvider(IWaveSource source): WaveStream
{
    public override WaveFormat WaveFormat => source.WaveFormat.ToNAudio();
    public override long Length => source.Length;
    public override long Position { get => source.Position; set => source.Position = value; }
    
    public override int Read(byte[] buffer, int offset, int count)
    {
        return source.Read(buffer, offset, count);
    }
}