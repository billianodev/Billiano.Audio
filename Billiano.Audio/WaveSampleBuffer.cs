#nullable disable
using System;
using System.Runtime.InteropServices;

namespace Billiano.Audio.FireForget;

[StructLayout(LayoutKind.Explicit)]
public struct WaveSampleBuffer
{
    [field: FieldOffset(0)]
    public int ByteBufferLength { get; }
    
    public int FloatBufferLength => ByteBufferLength / 4;
    
    [field: FieldOffset(8)]
    public byte[] ByteBuffer { get; }
    
    [field: FieldOffset(8)]
    public float[] FloatBuffer { get; }

    public WaveSampleBuffer(byte[] buffer)
    {
        var align = buffer.Length % 4;
        ByteBufferLength = align == 0 ? buffer.Length : buffer.Length - align + 4;

        Span<byte> copy = stackalloc byte[ByteBufferLength];
        buffer.CopyTo(copy);
        
        ByteBuffer = copy.ToArray();
    }

    public WaveSampleBuffer(float[] buffer)
    {
        FloatBuffer = buffer;
        ByteBufferLength = buffer.Length * 4;
    }
}