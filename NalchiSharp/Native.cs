// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace NalchiSharp;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using GnsSharp;

#pragma warning disable SA1300 // Element should begin with upper-case letter

internal static partial class Native
{
    /// <summary>
    /// Native library name.
    /// </summary>
#if NALCHI_SHARP_PLATFORM_WINDOWS
    public const string NalchiLibraryName = "nalchi";
#elif NALCHI_SHARP_PLATFORM_POSIX
    public const string NalchiLibraryName = "libnalchi";
#else
#error "Unknown native library name. Define `NALCHI_SHARP_PLATFORM_*` according to your platform."
#endif

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial SharedPayload nalchi_shared_payload_allocate(uint size);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_shared_payload_force_deallocate(SharedPayload payload);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_shared_payload_size(SharedPayload payload);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_shared_payload_word_ceiled_size(SharedPayload payload);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_shared_payload_internal_alloc_size(SharedPayload payload);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_shared_payload_used_bit_stream(SharedPayload payload);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr nalchi_bit_stream_writer_construct_default();

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr nalchi_bit_stream_writer_construct_with_shared_payload(SharedPayload buffer, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr nalchi_bit_stream_writer_construct_with_word_range(IntPtr begin, IntPtr end, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr nalchi_bit_stream_writer_construct_with_word_ptr_and_length(Span<uint> begin, uint words_length, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial IntPtr nalchi_bit_stream_writer_construct_with_word_ptr_and_length(uint* begin, uint words_length, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_writer_destroy(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_writer_set_fail(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_fail(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_writer_total_bytes(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_writer_total_bits(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_writer_used_bytes(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_writer_used_bits(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_writer_unused_bytes(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_writer_unused_bits(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_writer_restart(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_writer_reset(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_writer_reset_with_shared_payload(IntPtr self, SharedPayload buffer, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_writer_reset_with_word_range(IntPtr self, IntPtr begin, IntPtr end, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_writer_reset_with_word_ptr_and_length(IntPtr self, Span<uint> begin, uint words_length, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial void nalchi_bit_stream_writer_reset_with_word_ptr_and_length(IntPtr self, uint* begin, uint words_length, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_flush_final(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_flushed(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_bytes(IntPtr self, ReadOnlySpan<byte> data,
                                                              uint size);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_bool(IntPtr self, [MarshalAs(UnmanagedType.I1)] bool data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_s8(IntPtr self, sbyte data, sbyte min, sbyte max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_u8(IntPtr self, byte data, byte min, byte max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_s16(IntPtr self, short data, short min, short max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_u16(IntPtr self, ushort data, ushort min, ushort max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_s32(IntPtr self, int data, int min, int max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_u32(IntPtr self, uint data, uint min, uint max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_s64(IntPtr self, long data, long min, long max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_u64(IntPtr self, ulong data, ulong min, ulong max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_float(IntPtr self, float data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_double(IntPtr self, double data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_ordinary_string(IntPtr self, [MarshalAs(UnmanagedType.LPStr)] string str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_wide_string(IntPtr self, [MarshalAs(UnmanagedType.LPWStr)] string str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_utf8_string(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_utf16_string(IntPtr self, [MarshalAs(UnmanagedType.LPWStr)] string str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_writer_write_utf32_string(IntPtr self, IntPtr str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr nalchi_bit_stream_measurer_construct();

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_destroy(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_measurer_used_bytes(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_measurer_used_bits(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_restart(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_bytes(IntPtr self, ReadOnlySpan<byte> data, uint size);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_bool(IntPtr self, [MarshalAs(UnmanagedType.I1)] bool data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_s8(IntPtr self, sbyte data, sbyte min, sbyte max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_u8(IntPtr self, byte data, byte min, byte max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_s16(IntPtr self, short data, short min, short max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_u16(IntPtr self, ushort data, ushort min, ushort max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_s32(IntPtr self, int data, int min, int max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_u32(IntPtr self, uint data, uint min, uint max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_s64(IntPtr self, long data, long min, long max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_u64(IntPtr self, ulong data, ulong min, ulong max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_float(IntPtr self, float data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_double(IntPtr self, double data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_ordinary_string(IntPtr self, [MarshalAs(UnmanagedType.LPStr)] string str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_wide_string(IntPtr self, [MarshalAs(UnmanagedType.LPWStr)] string str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_utf8_string(IntPtr self, [MarshalAs(UnmanagedType.LPUTF8Str)] string str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_utf16_string(IntPtr self, [MarshalAs(UnmanagedType.LPWStr)] string str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_measurer_write_utf32_string(IntPtr self, IntPtr str);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr nalchi_bit_stream_reader_construct_default();

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr nalchi_bit_stream_reader_construct_with_word_range(IntPtr begin, IntPtr end, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr nalchi_bit_stream_reader_construct_with_word_ptr_and_length(ReadOnlySpan<uint> begin, uint words_length, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial IntPtr nalchi_bit_stream_reader_construct_with_word_ptr_and_length(uint* begin, uint words_length, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_reader_destroy(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_reader_set_fail(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_fail(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_reader_total_bytes(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_reader_total_bits(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_reader_used_bytes(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_reader_used_bits(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_reader_unused_bytes(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial uint nalchi_bit_stream_reader_unused_bits(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_reader_restart(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_reader_reset(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_reader_reset_with_word_range(IntPtr self, IntPtr begin, IntPtr end, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_bit_stream_reader_reset_with_word_ptr_and_length(IntPtr self, ReadOnlySpan<uint> begin, uint words_length, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial void nalchi_bit_stream_reader_reset_with_word_ptr_and_length(IntPtr self, uint* begin, uint words_length, uint logical_bytes_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_bytes(IntPtr self, Span<byte> data, uint size);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_bool(IntPtr self, [MarshalAs(UnmanagedType.I1)] out bool data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_s8(IntPtr self, out sbyte data, sbyte min, sbyte max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_u8(IntPtr self, out byte data, byte min, byte max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_s16(IntPtr self, out short data,
                                                           short min, short max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_u16(IntPtr self, out ushort data,
                                                           ushort min, ushort max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_s32(IntPtr self, out int data,
                                                           int min, int max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_u32(IntPtr self, out uint data,
                                                           uint min, uint max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_s64(IntPtr self, out long data,
                                                           long min, long max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_u64(IntPtr self, out ulong data,
                                                           ulong min, ulong max);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_float(IntPtr self, out float data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_double(IntPtr self, out double data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_ordinary_string(IntPtr self, Span<byte> str,
                                                                       uint max_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_wide_string(IntPtr self, Span<byte> str,
                                                                   uint max_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_utf8_string(IntPtr self, Span<byte> str,
                                                                   uint max_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_utf16_string(IntPtr self, Span<ushort> str,
                                                                    uint max_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool nalchi_bit_stream_reader_read_utf32_string(IntPtr self, Span<uint> str,
                                                                    uint max_length);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int nalchi_bit_stream_reader_peek_string_length(IntPtr self);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_socket_extensions_unicast(IntPtr sockets, HSteamNetConnection connection, SharedPayload payload, int logical_bytes_length, ESteamNetworkingSendType send_flags, out long out_message_number_or_result, ushort lane, long user_data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_socket_extensions_unicast(IntPtr sockets, HSteamNetConnection connection, SharedPayload payload, int logical_bytes_length, ESteamNetworkingSendType send_flags, IntPtr out_message_number_or_result, ushort lane, long user_data);

    [LibraryImport(NalchiLibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void nalchi_socket_extensions_multicast(IntPtr sockets, uint connections_count, ReadOnlySpan<HSteamNetConnection> connections, SharedPayload payload, int logical_bytes_length, ESteamNetworkingSendType send_flags, Span<long> out_message_number_or_result, ushort lane, long user_data);
}
