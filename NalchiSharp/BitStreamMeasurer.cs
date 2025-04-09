// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace NalchiSharp;

using System;

/// <summary>
/// <para>
/// Measures the bytes <see cref="BitStreamWriter"/> will use.
/// </para>
/// <para>
/// This never actually writes any data.<br/>
/// Instead, it only measures how many bytes <see cref="BitStreamWriter"/> would use.<br/>
/// You can use this to measure the required space for the <see cref="BitStreamWriter"/>.
/// </para>
/// </summary>
public sealed class BitStreamMeasurer : IDisposable
{
    private IntPtr ptr;

    /// <summary>
    /// Constructs a <see cref="BitStreamMeasurer"/> instance.
    /// </summary>
    public BitStreamMeasurer()
    {
        this.ptr = Native.nalchi_bit_stream_measurer_construct();
    }

    /// <summary>
    /// Destroys the <see cref="BitStreamMeasurer"/> instance.
    /// </summary>
    ~BitStreamMeasurer() => this.Dispose();

    /// <summary>
    /// Gets the number of used (measured) bytes.
    /// </summary>
    /// <returns><see cref="uint"/> Number of used (measured) bytes.</returns>
    public uint UsedBytes => Native.nalchi_bit_stream_measurer_used_bytes(this.ptr);

    /// <summary>
    /// Gets the number of used (measured) bits.
    /// </summary>
    /// <returns><see cref="uint"/> Number of used (measured) bits.</returns>
    public uint UsedBits => Native.nalchi_bit_stream_measurer_used_bits(this.ptr);

    /// <summary>
    /// Disposes the <see cref="BitStreamMeasurer"/> instance.
    /// </summary>
    public void Dispose()
    {
        if (this.ptr != IntPtr.Zero)
        {
            Native.nalchi_bit_stream_measurer_destroy(this.ptr);
            this.ptr = IntPtr.Zero;

            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Restarts the measure from zero.
    /// </summary>
    public void Restart()
    {
        Native.nalchi_bit_stream_measurer_restart(this.ptr);
    }

    /// <summary>
    /// Fake-writes some arbitrary data to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(ReadOnlySpan<byte> data)
    {
        Native.nalchi_bit_stream_measurer_write_bytes(this.ptr, data, (uint)data.Length);
        return this;
    }

    /// <summary>
    /// Fake-writes a <see cref="bool"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(bool data)
    {
        Native.nalchi_bit_stream_measurer_write_bool(this.ptr, data);
        return this;
    }

    /// <summary>
    /// Fake-writes a <see cref="sbyte"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(sbyte data, sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
    {
        Native.nalchi_bit_stream_measurer_write_s8(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Fake-writes a <see cref="byte"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(byte data, byte min = byte.MinValue, byte max = byte.MaxValue)
    {
        Native.nalchi_bit_stream_measurer_write_u8(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Fake-writes a <see cref="short"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(short data, short min = short.MinValue, short max = short.MaxValue)
    {
        Native.nalchi_bit_stream_measurer_write_s16(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Fake-writes an <see cref="ushort"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(ushort data, ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
    {
        Native.nalchi_bit_stream_measurer_write_u16(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Fake-writes an <see cref="int"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(int data, int min = int.MinValue, int max = int.MaxValue)
    {
        Native.nalchi_bit_stream_measurer_write_s32(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Fake-writes an <see cref="uint"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(uint data, uint min = uint.MinValue, uint max = uint.MaxValue)
    {
        Native.nalchi_bit_stream_measurer_write_u32(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Fake-writes a <see cref="long"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(long data, long min = long.MinValue, long max = long.MaxValue)
    {
        Native.nalchi_bit_stream_measurer_write_s64(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Fake-writes an <see cref="ulong"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <param name="min">Minimum value allowed for <paramref name="data"/>.</param>
    /// <param name="max">Maximum value allowed for <paramref name="data"/>.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(ulong data, ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
    {
        Native.nalchi_bit_stream_measurer_write_u64(this.ptr, data, min, max);
        return this;
    }

    /// <summary>
    /// Fake-writes a <see cref="float"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(float data)
    {
        Native.nalchi_bit_stream_measurer_write_float(this.ptr, data);
        return this;
    }

    /// <summary>
    /// Fake-writes a <see cref="double"/> value to the bit stream.
    /// </summary>
    /// <param name="data">Data to fake-write.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer Write(double data)
    {
        Native.nalchi_bit_stream_measurer_write_double(this.ptr, data);
        return this;
    }

    /// <summary>
    /// Fake-writes a UTF-8 string to the bit stream.
    /// </summary>
    /// <param name="str">String to fake-write.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer WriteUtf8String(string str)
    {
        Native.nalchi_bit_stream_measurer_write_utf8_string(this.ptr, str);
        return this;
    }

    /// <summary>
    /// Fake-writes a UTF-16 string to the bit stream.
    /// </summary>
    /// <param name="str">String to fake-write.</param>
    /// <returns><see cref="BitStreamMeasurer"/> The stream itself.</returns>
    public BitStreamMeasurer WriteUtf16String(string str)
    {
        Native.nalchi_bit_stream_measurer_write_utf16_string(this.ptr, str);
        return this;
    }
}
