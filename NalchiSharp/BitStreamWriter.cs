// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace NalchiSharp;

using System;

/// <summary>
/// <para>
/// Helper stream to write bits to your buffer.
/// </para>
/// <para>
/// Its design is based on the articles by Glenn Fiedler, see:<br/>
/// * https://gafferongames.com/post/reading_and_writing_packets/<br/>
/// * https://gafferongames.com/post/serialization_strategies/
/// </para>
/// </summary>
/// <remarks>
/// NOTE: <see cref="BitStreamWriter"/> uses an internal scratch buffer,
/// so the final few bytes might not be flushed to your buffer yet when you're done writing.<br/>
/// So, after writing everything, you <b>must</b> call <see cref="FinalFlush"/> to flush the remaining bytes to your buffer.<br/>
/// (Destroying the <see cref="BitStreamWriter"/> instance won't flush them, either.)
/// </remarks>
public sealed class BitStreamWriter : IDisposable
{
    /// <summary>
    /// Size of the internal word type.
    /// </summary>
    public const int WordSize = sizeof(uint);

    private IntPtr ptr;

    /// <summary>
    /// <para>
    /// Constructs a <see cref="BitStreamWriter"/> instance without a buffer.
    /// </para>
    /// <para>
    /// This constructor can be useful if you want to set the buffer afterwards.<br/>
    /// To set the buffer, call <see cref="ResetWith(SharedPayload, uint)"/>.
    /// </para>
    /// </summary>
    public BitStreamWriter()
    {
        this.ptr = Native.nalchi_bit_stream_writer_construct_default();
    }

    /// <summary>
    /// Constructs a <see cref="BitStreamWriter"/> instance with a <see cref="SharedPayload"/> buffer.
    /// </summary>
    /// <param name="buffer">Buffer to write bits to.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial write to the final word.</param>
    public BitStreamWriter(SharedPayload buffer, uint logicalBytesLength)
    {
        this.ptr = Native.nalchi_bit_stream_writer_construct_with_shared_payload(buffer, logicalBytesLength);
    }

    /// <summary>
    /// Constructs a <see cref="BitStreamWriter"/> instance with a <see cref="uint"/> <see cref="Span{T}"/> buffer.
    /// </summary>
    /// <remarks>
    /// NOTE: You need to ensure the <paramref name="buffer"/> is fixed, as its pointer is stored inside of native class.
    /// </remarks>
    /// <param name="buffer">Buffer to write bits to.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial write to the final word.</param>
    public BitStreamWriter(Span<uint> buffer, uint logicalBytesLength)
    {
        this.ptr = Native.nalchi_bit_stream_writer_construct_with_word_ptr_and_length(buffer, (uint)buffer.Length, logicalBytesLength);
    }

    /// <summary>
    /// Constructs a <see cref="BitStreamWriter"/> instance with a word begin pointer and the word length.
    /// </summary>
    /// <param name="begin">Pointer to the beginning of a buffer.</param>
    /// <param name="wordsLength">Number of words in the buffer.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial write to the final word.</param>
    public unsafe BitStreamWriter(uint* begin, uint wordsLength, uint logicalBytesLength)
    {
        this.ptr = Native.nalchi_bit_stream_writer_construct_with_word_ptr_and_length(begin, wordsLength, logicalBytesLength);
    }

    /// <summary>
    /// Destroys the <see cref="BitStreamWriter"/> instance.
    /// </summary>
    ~BitStreamWriter() => this.Dispose();

    /// <summary>
    /// Gets the number of total bytes in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of total bytes in the stream.</returns>
    public uint TotalBytes => Native.nalchi_bit_stream_writer_total_bytes(this.ptr);

    /// <summary>
    /// Gets the number of total bits in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of total bits in the stream.</returns>
    public uint TotalBits => Native.nalchi_bit_stream_writer_total_bits(this.ptr);

    /// <summary>
    /// Gets the number of used bytes in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of used bytes in the stream.</returns>
    public uint UsedBytes => Native.nalchi_bit_stream_writer_used_bytes(this.ptr);

    /// <summary>
    /// Gets the number of used bits in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of used bits in the stream.</returns>
    public uint UsedBits => Native.nalchi_bit_stream_writer_used_bits(this.ptr);

    /// <summary>
    /// Gets the number of unused bytes in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of unused bytes in the stream.</returns>
    public uint UnusedBytes => Native.nalchi_bit_stream_writer_unused_bytes(this.ptr);

    /// <summary>
    /// Gets the number of unused bits in the stream.
    /// </summary>
    /// <returns><see cref="uint"/> Number of unused bits in the stream.</returns>
    public uint UnusedBits => Native.nalchi_bit_stream_writer_unused_bits(this.ptr);

    /// <summary>
    /// Checks if <see cref="FlushFinal"/> has been called or not.
    /// </summary>
    /// <returns><see cref="bool"/> Whether the <see cref="FlushFinal"/> has been called or not.</returns>
    public bool Flushed => Native.nalchi_bit_stream_writer_flushed(this.ptr);

    /// <summary>
    /// <para>
    /// Check if writing to your buffer has been failed or not.
    /// </para>
    /// <para>
    /// If this is <c>true</c>, all the operations for this <see cref="BitStreamWriter"/> is no-op.
    /// </para>
    /// </summary>
    /// <returns><see cref="bool"/> <c>true</c> if writing has been failed, otherwise <c>false</c>.</returns>
    public bool Fail => Native.nalchi_bit_stream_writer_fail(this.ptr);

    /// <summary>
    /// <para>
    /// Check if there was no error in the writing to your buffer.<br/>
    /// This is effectively same as !<see cref="Fail"/>.
    /// </para>
    /// <para>
    /// If this is <c>false</c>, all the operations for this <see cref="BitStreamWriter"/> is no-op.
    /// </para>
    /// </summary>
    public static implicit operator bool(BitStreamWriter writer)
    {
        return !writer.Fail;
    }

    /// <summary>
    /// Disposes the <see cref="BitStreamWriter"/> instance.
    /// </summary>
    public void Dispose()
    {
        if (this.ptr != IntPtr.Zero)
        {
            Native.nalchi_bit_stream_writer_destroy(this.ptr);
            this.ptr = IntPtr.Zero;

            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Force set the fail flag.
    /// </summary>
    public void SetFail()
    {
        Native.nalchi_bit_stream_writer_set_fail(this.ptr);
    }

    /// <summary>
    /// Restarts the stream so that it can write from the beginning again.
    /// </summary>
    /// <remarks>
    /// NOTE: This function resets internal states <b>without</b> flushing,
    /// so if you need flushing, you should call <see cref="FlushFinal"/> beforehand.
    /// </remarks>
    public void Restart()
    {
        Native.nalchi_bit_stream_writer_restart(this.ptr);
    }

    /// <summary>
    /// Resets the stream so that it no longer holds your buffer anymore.
    /// </summary>
    /// <remarks>
    /// NOTE: This function removes reference to your buffer <b>without</b> flushing,<br/>
    /// so if you need flushing, you should call <see cref="FlushFinal"/> beforehand.
    /// </remarks>
    public void Reset()
    {
        Native.nalchi_bit_stream_writer_reset(this.ptr);
    }

    /// <summary>
    /// Resets the stream with a <see cref="SharedPayload"/> buffer.
    /// </summary>
    /// <remarks>
    /// NOTE: This function resets to the new buffer <b>without</b> flushing to your previous buffer,<br/>
    /// so if you need flushing, you should call <see cref="FlushFinal"/> beforehand.
    /// </remarks>
    /// <param name="buffer">Buffer to write bits to.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial write to the final word.</param>
    public void ResetWith(SharedPayload buffer, uint logicalBytesLength)
    {
        Native.nalchi_bit_stream_writer_reset_with_shared_payload(this.ptr, buffer, logicalBytesLength);
    }

    /// <summary>
    /// Resets the stream with a <see cref="uint"/> <see cref="Span{T}"/> buffer.
    /// </summary>
    /// <remarks>
    /// <para>
    /// NOTE: This function resets to the new buffer <b>without</b> flushing to your previous buffer,<br/>
    /// so if you need flushing, you should call <see cref="FlushFinal"/> beforehand.
    /// </para>
    /// <para>
    /// NOTE: You need to ensure the <paramref name="buffer"/> is fixed, as its pointer is stored inside of native class.
    /// </para>
    /// </remarks>
    /// <param name="buffer">Buffer to write bits to.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial write to the final word.</param>
    public void ResetWith(Span<uint> buffer, uint logicalBytesLength)
    {
        Native.nalchi_bit_stream_writer_reset_with_word_ptr_and_length(this.ptr, buffer, (uint)buffer.Length, logicalBytesLength);
    }

    /// <summary>
    /// Resets the stream with a word begin pointer and the word length.
    /// </summary>
    /// <remarks>
    /// NOTE: This function resets to the new buffer <b>without</b> flushing to your previous buffer,<br/>
    /// so if you need flushing, you should call <see cref="FlushFinal"/> beforehand.
    /// </remarks>
    /// <param name="begin">Pointer to the beginning of a buffer.</param>
    /// <param name="wordsLength">Number of words in the buffer.</param>
    /// <param name="logicalBytesLength">Number of bytes logically. This is useful if you want to only allow partial write to the final word.</param>
    public unsafe void ResetWith(uint* begin, uint wordsLength, uint logicalBytesLength)
    {
        Native.nalchi_bit_stream_writer_reset_with_word_ptr_and_length(this.ptr, begin, wordsLength, logicalBytesLength);
    }

    /// <summary>
    /// Flushes the last remaining bytes on the internal scratch buffer to your buffer.
    /// </summary>
    /// <remarks>
    /// NOTE: This function must be only called when you're done writing.<br/>
    /// Any attempt to write more data after calling this function will set the fail flag and write nothing.
    /// </remarks>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter FlushFinal()
    {
        Native.nalchi_bit_stream_writer_flush_final(this.ptr);
        return this;
    }

    /// <summary>
    /// Writes some arbitrary data to the bit stream.
    /// </summary>
    /// <remarks>
    /// Bytes in your data could be read <b>swapped</b> if it is sent to the system with different endianness.<br/>
    /// So, prefer using other overloads instead.
    /// </remarks>
    /// <param name="data">Data to write.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(ReadOnlySpan<byte> data)
    {
        Native.nalchi_bit_stream_writer_write_bytes(this.ptr, data, (uint)data.Length);
        return this;
    }

    /// <summary>
    /// Writes a <see cref="bool"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(bool data)
    {
        Native.nalchi_bit_stream_writer_write_bool(this.ptr, data);
        return this;
    }

    /// <summary>
    /// Writes a <see cref="sbyte"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(sbyte data, sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
    {
        Native.nalchi_bit_stream_writer_write_s8(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Writes a <see cref="byte"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(byte data, byte min = byte.MinValue, byte max = byte.MaxValue)
    {
        Native.nalchi_bit_stream_writer_write_u8(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Writes a <see cref="short"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(short data, short min = short.MinValue, short max = short.MaxValue)
    {
        Native.nalchi_bit_stream_writer_write_s16(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Writes an <see cref="ushort"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(ushort data, ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
    {
        Native.nalchi_bit_stream_writer_write_u16(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Writes an <see cref="int"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(int data, int min = int.MinValue, int max = int.MaxValue)
    {
        Native.nalchi_bit_stream_writer_write_s32(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Writes an <see cref="uint"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(uint data, uint min = uint.MinValue, uint max = uint.MaxValue)
    {
        Native.nalchi_bit_stream_writer_write_u32(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Writes a <see cref="long"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(long data, long min = long.MinValue, long max = long.MaxValue)
    {
        Native.nalchi_bit_stream_writer_write_s64(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Writes an <see cref="ulong"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(ulong data, ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
    {
        Native.nalchi_bit_stream_writer_write_u64(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Writes a <see cref="float"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(float data)
    {
        Native.nalchi_bit_stream_writer_write_float(this.ptr, data);
        return this;
    }

    /// <summary>
    /// Writes a <see cref="double"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to write.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter Write(double data)
    {
        Native.nalchi_bit_stream_writer_write_double(this.ptr, data);
        return this;
    }

    /// <summary>
    /// Writes a UTF-8 string to the bit stream.
    /// </summary>
    /// <param name="str">String to write.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter WriteUtf8String(string str)
    {
        Native.nalchi_bit_stream_writer_write_utf8_string(this.ptr, str);
        return this;
    }

    /// <summary>
    /// Writes a UTF-16 string to the bit stream.
    /// </summary>
    /// <param name="str">String to write.</param>
    /// <returns><see cref="BitStreamWriter"/> The stream itself.</returns>
    public BitStreamWriter WriteUtf16String(string str)
    {
        Native.nalchi_bit_stream_writer_write_utf16_string(this.ptr, str);
        return this;
    }
}
