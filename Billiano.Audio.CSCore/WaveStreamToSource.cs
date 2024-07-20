using CSCore;
using NAudio.Wave;

namespace Billiano.Audio;

public sealed class WaveStreamToSource(WaveStream source) : IWaveSource
{
    public bool CanSeek => source.CanSeek;

    public CSCore.WaveFormat WaveFormat => WaveFormatConverter.ToCSCore(source.WaveFormat);

    public long Position
    {
        get => source.Position;
        set => source.Position = value;
    }

    public long Length => source.Length;

    public int Read(byte[] buffer, int offset, int count)
    {
        return source.Read(buffer, offset, count);
    }

    public void Dispose()
    {
        source.Dispose();
    }
}