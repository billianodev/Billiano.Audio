using System;
using NAudio.Wave;

namespace Billiano.Audio;

public static class MemoryWaveExtensions
{
    public static byte[] ReadBytes(this WaveStream stream, int offset, int count)
    {
        var bytesAvailableToRead = stream.Length - stream.Position - offset;
        if (bytesAvailableToRead <= 0)
        {
            return [];
        }

        var bytesToRead = Math.Min(count, bytesAvailableToRead);

        var buffer = new byte[bytesToRead];
        var bytesRead = stream.Read(buffer, offset, buffer.Length);

        if (bytesRead != buffer.Length)
        {
            Array.Resize(ref buffer, bytesRead);
        }

        return buffer;
    }

    public static byte[] ReadToEnd(this WaveStream stream)
    {
        var bytesToRead = stream.Length - stream.Position;
        if (bytesToRead <= 0)
        {
            return [];
        }

        var buffer = new byte[bytesToRead];
        var bytesRead = stream.Read(buffer, 0, buffer.Length);

        if (bytesRead != buffer.Length)
        {
            Array.Resize(ref buffer, bytesRead);
        }

        return buffer;
    }
}
