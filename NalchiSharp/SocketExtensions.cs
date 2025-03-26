// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace NalchiSharp;

using System;
using GnsSharp;

#pragma warning disable CS0419 // Ambiguous reference in cref attribute

/// <summary>
/// Extensions for <see cref="ISteamNetworkingSockets"/>.
/// </summary>
public static class SocketExtensions
{
    /// <summary>
    /// <para>
    /// Unicasts a <see cref="SharedPayload"/> to a connection.
    /// </para>
    /// <para>
    /// This function is pretty similar to the <see cref="ISteamNetworkingSockets.SendMessageToConnection"/>, but it's for the <see cref="SharedPayload"/>.<br/>
    /// But, it uses <see cref="ISteamNetworkingSockets.SendMessages"/> under the hood, so the result is returns with <paramref name="outMessageNumberOrResult"/> instead of a return value.
    /// </para>
    /// </summary>
    /// <param name="sockets">Steam sockets interface.</param>
    /// <param name="connection">Connection to send to.</param>
    /// <param name="payload">Payload to send.</param>
    /// <param name="logicalBytesLength">Logical number of bytes of the payload.</param>
    /// <param name="sendFlags">Send flags. See <a href="https://partner.steamgames.com/doc/api/steamnetworkingtypes#message_sending_flags">message sending flags</a> on the Steamworks docs.</param>
    /// <param name="outMessageNumberOrResult">Optional parameter to receive the message number if successful, or a negative <see cref="EResult"/> value if failed.</param>
    public static void Unicast(this ISteamNetworkingSockets sockets, HSteamNetConnection connection, SharedPayload payload, int logicalBytesLength, ESteamNetworkingSendType sendFlags, out long outMessageNumberOrResult)
    {
        Native.nalchi_socket_extensions_unicast(sockets.Ptr, connection, payload, logicalBytesLength, sendFlags, out outMessageNumberOrResult);
    }

    /// <summary>
    /// <para>
    /// Unicasts a <see cref="SharedPayload"/> to a connection.
    /// </para>
    /// <para>
    /// This function is pretty similar to the <see cref="ISteamNetworkingSockets.SendMessageToConnection"/>, but it's for the <see cref="SharedPayload"/>.<br/>
    /// But, it uses <see cref="ISteamNetworkingSockets.SendMessages"/> under the hood, so the result is returns with <c>outMessageNumberOrResult</c> instead of a return value.
    /// </para>
    /// </summary>
    /// <param name="sockets">Steam sockets interface.</param>
    /// <param name="connection">Connection to send to.</param>
    /// <param name="payload">Payload to send.</param>
    /// <param name="logicalBytesLength">Logical number of bytes of the payload.</param>
    /// <param name="sendFlags">Send flags. See <a href="https://partner.steamgames.com/doc/api/steamnetworkingtypes#message_sending_flags">message sending flags</a> on the Steamworks docs.</param>
    public static void Unicast(this ISteamNetworkingSockets sockets, HSteamNetConnection connection, SharedPayload payload, int logicalBytesLength, ESteamNetworkingSendType sendFlags)
    {
        Native.nalchi_socket_extensions_unicast(sockets.Ptr, connection, payload, logicalBytesLength, sendFlags, IntPtr.Zero);
    }

    /// <summary>
    /// <para>
    /// Multicasts a <see cref="SharedPayload"/> to the connections.
    /// </para>
    /// <para>
    /// This function uses <see cref="ISteamNetworkingSockets.SendMessages"/> under the hood, but it shares the payload between them.<br/>
    /// So, it's more efficient if you send a same message to a lot of connections with this.
    /// </para>
    /// </summary>
    /// <param name="sockets">Steam sockets interface.</param>
    /// <param name="connections">Connections to multicast to.</param>
    /// <param name="payload">Payload to send.</param>
    /// <param name="logicalBytesLength">Logical number of bytes of the payload.</param>
    /// <param name="sendFlags">Send flags. See <a href="https://partner.steamgames.com/doc/api/steamnetworkingtypes#message_sending_flags">message sending flags</a> on the Steamworks docs.</param>
    /// <param name="outMessageNumberOrResult">Optional parameter to receive the message number if successful, or a negative <see cref="EResult"/> value if failed.</param>
    public static void Multicast(this ISteamNetworkingSockets sockets, ReadOnlySpan<HSteamNetConnection> connections, SharedPayload payload, int logicalBytesLength, ESteamNetworkingSendType sendFlags, Span<long> outMessageNumberOrResult)
    {
        Native.nalchi_socket_extensions_multicast(sockets.Ptr, (uint)connections.Length, connections, payload, logicalBytesLength, sendFlags, outMessageNumberOrResult);
    }

    /// <summary>
    /// <para>
    /// Multicasts a <see cref="SharedPayload"/> to the connections.
    /// </para>
    /// <para>
    /// This function uses <see cref="ISteamNetworkingSockets.SendMessages"/> under the hood, but it shares the payload between them.<br/>
    /// So, it's more efficient if you send a same message to a lot of connections with this.
    /// </para>
    /// </summary>
    /// <param name="sockets">Steam sockets interface.</param>
    /// <param name="connections">Connections to multicast to.</param>
    /// <param name="payload">Payload to send.</param>
    /// <param name="logicalBytesLength">Logical number of bytes of the payload.</param>
    /// <param name="sendFlags">Send flags. See <a href="https://partner.steamgames.com/doc/api/steamnetworkingtypes#message_sending_flags">message sending flags</a> on the Steamworks docs.</param>
    public static void Multicast(this ISteamNetworkingSockets sockets, ReadOnlySpan<HSteamNetConnection> connections, SharedPayload payload, int logicalBytesLength, ESteamNetworkingSendType sendFlags)
    {
        Multicast(sockets, connections, payload, logicalBytesLength, sendFlags, []);
    }
}
