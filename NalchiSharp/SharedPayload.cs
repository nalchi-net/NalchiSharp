// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace NalchiSharp;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// <para>
/// Shared payload to store data to send.
/// </para>
///
/// <para>
/// The payload is "shared" when it is used for multicast.
/// </para>
/// </summary>
/// <remarks>
/// NOTE: As <see cref="Ptr"/> has a hidden reference count &amp; alloc size fields on front,
/// you <b>can't</b> just use your own buffer as a <see cref="Ptr"/>;<br/>
/// You need to call <see cref="Allocate"/> to allocate the <see cref="SharedPayload"/>.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct SharedPayload
{
    /// <summary>
    /// Pointer to the payload, allocated by nalchi.
    /// </summary>
    public IntPtr Ptr;

    /// <summary>
    /// Gets the requested allocation size of the payload.
    /// </summary>
    /// <returns>
    /// Size of the payload in bytes.
    /// </returns>
    public readonly uint Size => Native.nalchi_shared_payload_size(this);

    /// <summary>
    /// <para>
    /// Gets the payload size that's ceiled to <see cref="BitStreamWriter"/>'s word size (4 bytes),
    /// which is guaranteed to be safe to access.
    /// </para>
    /// <para>
    /// As this size is ceiled to multiple of <see cref="BitStreamWriter"/>'s word size,
    /// it can be bigger than the requested allocation size. <br/>
    /// This is to maintain compatibility with <see cref="BitStreamWriter"/>.
    /// </para>
    /// </summary>
    /// <returns>
    /// Size of the payload in bytes.
    /// </returns>
    public readonly uint WordCeiledSize => Native.nalchi_shared_payload_word_ceiled_size(this);

    /// <summary>
    /// <para>
    /// Gets the actual allocated size,
    /// which includes hidden ref count &amp; size fields.
    /// </para>
    /// <para>
    /// This is meant to be only used by the internal API.
    /// </para>
    /// </summary>
    /// <returns>
    /// Size of the allocated space in bytes.
    /// </returns>
    public readonly uint InternalAllocSize => Native.nalchi_shared_payload_internal_alloc_size(this);

    /// <summary>
    /// <para>
    /// Check if this payload used <see cref="BitStreamWriter"/> to fill its content.
    /// </para>
    /// <para>
    /// If this is <c>true</c>, the send size will be automatically
    /// ceiled to multiple of <see cref="BitStreamWriter"/>'s word size (4 bytes) when you send it. <br/>
    /// This is to maintain compatibility with <see cref="BitStreamReader"/> on the receiving side.
    /// </para>
    /// <returns>
    /// Whether the payload used <see cref="BitStreamWriter"/> or not.
    /// </returns>
    /// </summary>
    public readonly bool UsedBitStream => Native.nalchi_shared_payload_used_bit_stream(this);

    /// <summary>
    /// Allocates a shared payload that can be used to send some data.
    /// </summary>
    /// <remarks>
    /// NOTE: You should check if <see cref="Ptr"/> is <see cref="IntPtr.Zero"/> or not to see if the allocation has been successful.
    /// </remarks>
    /// <param name="size">Space in bytes to allocate.</param>
    /// <returns>Shared payload instance that might hold allocated buffer.</returns>
    public static SharedPayload Allocate(uint size)
    {
        return Native.nalchi_shared_payload_allocate(size);
    }

    /// <summary>
    /// Force deallocates the shared payload without sending it.
    /// </summary>
    /// <remarks>
    /// <para>
    /// NOTE: If you send the payload, nalchi takes the ownership of the payload and releases it automatically.<br/>
    /// So, you should <b>not</b> call this if you already sent the payload.
    /// </para>
    /// <para>
    /// Calling this is only necessary when you have some exceptions in your program
    /// that prevents sending the allocated payload.
    /// </para>
    /// </remarks>
    /// <param name="payload">Shared payload to force deallocate.</param>
    public static void ForceDeallocate(SharedPayload payload)
    {
        Native.nalchi_shared_payload_force_deallocate(payload);
    }
}
