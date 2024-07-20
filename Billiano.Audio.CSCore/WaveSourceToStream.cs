using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

public sealed class WaveSourceToStream(IWaveSource source) : WaveStream
{
    public override NAudio.Wave.WaveFormat WaveFormat => WaveFormatConverter.ToNAudio(source.WaveFormat);

    public override long Length => source.Length;

    public override long Position
    {
        get => source.Position;
        set => source.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return source.Read(buffer, offset, count);
    }
}