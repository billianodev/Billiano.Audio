#nullable disable
using System;
using System.Runtime.InteropServices;

namespace Billiano.Audio.FireForget;

/// <summary>
/// Convert between bytes and float array
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct WaveSampleBuffer
{
    /// <summary>
    /// 
    /// </summary>
    [field: FieldOffset(0)]
    public int ByteBufferLength { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public int FloatBufferLength => ByteBufferLength / 4;
    
    /// <summary>
    /// 
    /// </summary>
    [field: FieldOffset(8)]
    public byte[] ByteBuffer { get; }
    
    /// <summary>
    /// 
    /// </summary>
    [field: FieldOffset(8)]
    public float[] FloatBuffer { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    public WaveSampleBuffer(byte[] buffer)
    {
        var bufferLength = buffer.Length;
        var align = bufferLength % 4;
        ByteBufferLength = align == 0 ? bufferLength : bufferLength - align + 4;

        var copy = (Span<byte>)stackalloc byte[ByteBufferLength];
        buffer.CopyTo(copy);
        
        ByteBuffer = copy.ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    public WaveSampleBuffer(ref byte[] buffer)
    {
        var bufferLength = buffer.Length;
        var align = bufferLength % 4;
        ByteBufferLength = align == 0 ? bufferLength : bufferLength - align + 4;
        
        Array.Resize(ref buffer, ByteBufferLength);

        ByteBuffer = buffer;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    public WaveSampleBuffer(float[] buffer)
    {
        FloatBuffer = buffer;
        ByteBufferLength = buffer.Length * 4;
    }
}