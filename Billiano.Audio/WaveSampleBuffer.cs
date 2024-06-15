#nullable disable
using System;
using System.Runtime.InteropServices;

namespace Billiano.Audio.FireForget;

/// <summary>
/// 
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct WaveSampleBuffer
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
        var align = buffer.Length % 4;
        ByteBufferLength = align == 0 ? buffer.Length : buffer.Length - align + 4;

        var copy = (Span<byte>)stackalloc byte[ByteBufferLength];
        buffer.CopyTo(copy);
        
        ByteBuffer = copy.ToArray();
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