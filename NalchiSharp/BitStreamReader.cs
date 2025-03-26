// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace NalchiSharp;

using System;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// <para>
/// Helper stream to read bits from your buffer.
/// </para>
/// <para>
/// Its design is based on the articles by Glenn Fiedler, see:<br/>
/// * https://gafferongames.com/post/reading_and_writing_packets/<br/>
/// * https://gafferongames.com/post/serialization_strategies/
/// </para>
/// </summary>
public sealed class BitStreamReader : IDisposable
{
    private IntPtr ptr;

    private bool disposed;

    /// <summary>
    /// <para>
    /// Constructs a <see cref="BitStreamReader"/> instance without a buffer.
    /// </para>
    /// <para>
    /// This constructor can be useful if you want to set the buffer afterwards.<br/>
    /// To set the buffer, call <see cref="ResetWith(SharedPayload, uint)"/>.
    /// </para>
    /// </summary>
    public BitStreamReader()
    {
        this.ptr = Native.nalchi_bit_stream_reader_construct_default();
    }

    /// <summary>
    /// Constructs a <see cref="BitStreamReader"/> instance with a <see cref="uint"/> <see cref="ReadOnlySpan{T}"/> buffer.
    /// </summary>
    /// <remarks>
    /// NOTE: You need to ensure the <paramref name="buffer"/> is fixed, as its pointer is stored inside of native class.
    /// </remarks>
    /// <param name="buffer">Buffer to read bits from.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial read from the final word.</param>
    public BitStreamReader(ReadOnlySpan<uint> buffer, uint logicalBytesLength)
    {
        this.ptr = Native.nalchi_bit_stream_reader_construct_with_word_ptr_and_length(buffer, (uint)buffer.Length, logicalBytesLength);
    }

    /// <summary>
    /// Constructs a `BitStreamReader` instance with a word begin pointer and the word length.
    /// </summary>
    /// <param name="begin"> Pointer to the beginning of a buffer.</param>
    /// <param name="wordsLength">Number of words in the buffer.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial read from the final word.</param>
    public unsafe BitStreamReader(uint* begin, uint wordsLength, uint logicalBytesLength)
    {
        this.ptr = Native.nalchi_bit_stream_reader_construct_with_word_ptr_and_length(begin, wordsLength, logicalBytesLength);
    }

    /// <summary>
    /// Destroys the <see cref="BitStreamReader"/> instance.
    /// </summary>
    ~BitStreamReader() => this.Dispose();

    /// <summary>
    /// Gets the number of total bytes in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of total bytes in the stream.</returns>
    public uint TotalBytes => Native.nalchi_bit_stream_reader_total_bytes(this.ptr);

    /// <summary>
    /// Gets the number of total bits in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of total bits in the stream.</returns>
    public uint TotalBits => Native.nalchi_bit_stream_reader_total_bits(this.ptr);

    /// <summary>
    /// Gets the number of used bytes in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of used bytes in the stream.</returns>
    public uint UsedBytes => Native.nalchi_bit_stream_reader_used_bytes(this.ptr);

    /// <summary>
    /// Gets the number of used bits in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of used bits in the stream.</returns>
    public uint UsedBits => Native.nalchi_bit_stream_reader_used_bits(this.ptr);

    /// <summary>
    /// Gets the number of unused bytes in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of unused bytes in the stream.</returns>
    public uint UnusedBytes => Native.nalchi_bit_stream_reader_unused_bytes(this.ptr);

    /// <summary>
    /// Gets the number of unused bits in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of unused bits in the stream.</returns>
    public uint UnusedBits => Native.nalchi_bit_stream_reader_unused_bits(this.ptr);

    /// <summary>
    /// <para>
    /// Check if reading from your buffer has been failed or not.
    /// </para>
    /// <para>
    /// If this is <c>true</c>, all the operations for this <see cref="BitStreamReader"/> is no-op.
    /// </para>
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if reading has been failed, otherwise <c>false</c>.</returns>
    public bool Fail => Native.nalchi_bit_stream_reader_fail(this.ptr);

    /// <summary>
    /// <para>
    /// Check if there was no error in the reading from your buffer.<br/>
    /// This is effectively same as !<see cref="Fail"/>.
    /// </para>
    /// <para>
    /// If this is <c>false</c>, all the operations for this <see cref="BitStreamReader"/> is no-op.
    /// </para>
    /// </summary>
    public static implicit operator bool(BitStreamReader reader)
    {
        return !reader.Fail;
    }

    /// <summary>
    /// Disposes the <see cref="BitStreamReader"/> instance.
    /// </summary>
    public void Dispose()
    {
        if (!this.disposed)
        {
            Native.nalchi_bit_stream_reader_destroy(this.ptr);
            this.ptr = IntPtr.Zero;

            GC.SuppressFinalize(this);
            this.disposed = true;
        }
    }

    /// <summary>
    /// Force set the fail flag.
    /// </summary>
    public void SetFail()
    {
        Native.nalchi_bit_stream_reader_set_fail(this.ptr);
    }

    /// <summary>
    /// Restarts the stream so that it can read from the beginning again.
    /// </summary>
    public void Restart()
    {
        Native.nalchi_bit_stream_reader_restart(this.ptr);
    }

    /// <summary>
    /// Resets the stream so that it no longer holds your buffer anymore.
    /// </summary>
    public void Reset()
    {
        Native.nalchi_bit_stream_reader_reset(this.ptr);
    }

    /// <summary>
    /// Resets the stream with a <see cref="uint"/> <see cref="Span{T}"/> buffer.
    /// </summary>
    /// <remarks>
    /// NOTE: You need to ensure the <paramref name="buffer"/> is fixed, as its pointer is stored inside of native class.
    /// </remarks>
    /// <param name="buffer">Buffer to read bits from.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial read from the final word.</param>
    public void ResetWith(ReadOnlySpan<uint> buffer, uint logicalBytesLength)
    {
        Native.nalchi_bit_stream_reader_reset_with_word_ptr_and_length(this.ptr, buffer, (uint)buffer.Length, logicalBytesLength);
    }

    /// <summary>
    /// Resets the stream with a word begin pointer and the word length.
    /// </summary>
    /// <param name="begin">Pointer to the beginning of a buffer.</param>
    /// <param name="wordsLength">Number of words in the buffer.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial read from the final word.</param>
    public unsafe void ResetWith(uint* begin, uint wordsLength, uint logicalBytesLength)
    {
        Native.nalchi_bit_stream_reader_reset_with_word_ptr_and_length(this.ptr, begin, wordsLength, logicalBytesLength);
    }

    /// <summary>
    /// Reads some arbitrary data from the bit stream.
    /// </summary>
    /// <remarks>
    /// NOTE: You could read <b>swapped</b> bytes if the data came from the system with different endianness.<br/>
    /// So, prefer using other overloads instead.
    /// </remarks>
    /// <param name="data">Data to read to.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(Span<byte> data)
    {
        Native.nalchi_bit_stream_reader_read_bytes(this.ptr, data, (uint)data.Length);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="bool"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out bool data)
    {
        Native.nalchi_bit_stream_reader_read_bool(this.ptr, out data);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="sbyte"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out sbyte data, sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
    {
        Native.nalchi_bit_stream_reader_read_s8(this.ptr, out data, min, max);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="byte"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out byte data, byte min = byte.MinValue, byte max = byte.MaxValue)
    {
        Native.nalchi_bit_stream_reader_read_u8(this.ptr, out data, min, max);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="short"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out short data, short min = short.MinValue, short max = short.MaxValue)
    {
        Native.nalchi_bit_stream_reader_read_s16(this.ptr, out data, min, max);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="ushort"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out ushort data, ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
    {
        Native.nalchi_bit_stream_reader_read_u16(this.ptr, out data, min, max);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="int"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out int data, int min = int.MinValue, int max = int.MaxValue)
    {
        Native.nalchi_bit_stream_reader_read_s32(this.ptr, out data, min, max);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="uint"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out uint data, uint min = uint.MinValue, uint max = uint.MaxValue)
    {
        Native.nalchi_bit_stream_reader_read_u32(this.ptr, out data, min, max);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="long"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out long data, long min = long.MinValue, long max = long.MaxValue)
    {
        Native.nalchi_bit_stream_reader_read_s64(this.ptr, out data, min, max);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="ulong"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out ulong data, ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
    {
        Native.nalchi_bit_stream_reader_read_u64(this.ptr, out data, min, max);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="float"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out float data)
    {
        Native.nalchi_bit_stream_reader_read_float(this.ptr, out data);
        return this;
    }

    /// <summary>
    /// Reads a <see cref="double"/> value from the bit stream.
    /// </summary>
    /// <param name="data">Data to read to.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader Read(out double data)
    {
        Native.nalchi_bit_stream_reader_read_double(this.ptr, out data);
        return this;
    }

    /// <summary>
    /// <para>
    /// Reads an UTF-8 string from the bit stream.
    /// </para>
    /// <para>
    /// If the length prefix for current stream position exceeds <paramref name="maxLength"/>,<br/>
    /// this function will set the fail flag and read nothing.
    /// </para>
    /// </summary>
    /// <param name="str">String to read to.</param>
    /// <param name="maxLength">Maximum number of <c>char8_t</c> that can be read.<br/>
    /// This is to prevent a huge allocation when a malicious message requests it.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader ReadUtf8String(out string? str, uint maxLength)
    {
        // Peek the length of the string.
        int len = this.PeekStringLength();
        if (len < 0 || len > maxLength)
        {
            str = null;
            this.SetFail();
            return this;
        }

        // Allocate the raw buffer to read to.
        Span<byte> raw = stackalloc byte[len + 1];

        // Read the raw string.
        if (!Native.nalchi_bit_stream_reader_read_utf8_string(this.ptr, raw, maxLength))
        {
            str = null;
            this.SetFail();
            return this;
        }

        // Out to managed string.
        raw = raw[..len];
        str = Encoding.UTF8.GetString(raw);

        return this;
    }

    /// <summary>
    /// <para>
    /// Reads an UTF-16 string from the bit stream.
    /// </para>
    /// <para>
    /// If the length prefix for current stream position exceeds <paramref name="maxLength"/>,<br/>
    /// this function will set the fail flag and read nothing.
    /// </para>
    /// </summary>
    /// <param name="str">String to read to.</param>
    /// <param name="maxLength">Maximum number of <c>char16_t</c> that can be read.<br/>
    /// This is to prevent a huge allocation when a malicious message requests it.</param>
    /// <returns><see cref="BitStreamReader"/> The stream itself.</returns>
    public BitStreamReader ReadUtf16String(out string? str, uint maxLength)
    {
        // Peek the length of the string.
        int len = this.PeekStringLength();
        if (len < 0 || len > maxLength)
        {
            str = null;
            this.SetFail();
            return this;
        }

        // Allocate the raw buffer to read to.
        Span<ushort> raw = stackalloc ushort[len + 1];

        // Read the raw string.
        if (!Native.nalchi_bit_stream_reader_read_utf16_string(this.ptr, raw, maxLength))
        {
            str = null;
            this.SetFail();
            return this;
        }

        // Out to managed string.
        raw = raw[..len];
        str = Encoding.Unicode.GetString(MemoryMarshal.AsBytes(raw));

        return this;
    }

    /// <summary>
    /// <para>
    /// Peeks the string length prefix from the current stream position.
    /// </para>
    /// <para>
    /// If it fails to read a string length prefix,<br/>
    /// this function will return a negative value and set the fail flag.
    /// </para>
    /// <remarks>
    /// NOTE: Be careful, if current stream position was not on the string length prefix, it might read garbage length!
    /// </remarks>
    /// </summary>
    /// <returns><see cref="int"/> Length of string in number of characters, or a negative value if length prefix is invalid.</returns>
    public int PeekStringLength()
    {
        return Native.nalchi_bit_stream_reader_peek_string_length(this.ptr);
    }
}
