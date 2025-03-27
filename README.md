# NalchiSharp

NalchiSharp is a C# binding for the [nalchi](https://github.com/nalchi-net/nalchi), which aims to provide utilities for efficient message sending over [ValveSoftware/GameNetworkingSockets](https://github.com/ValveSoftware/GameNetworkingSockets).

# Documentation

As this is a direct binding of the [nalchi](https://github.com/nalchi-net/nalchi), you can refer to the [nalchi API reference](https://nalchi-net.github.io/nalchi/) for more info.

There are some minor differences:
* [nalchi](https://github.com/nalchi-net/nalchi) is written in C++, so it uses `snake_case` mostly, while NalchiSharp uses `PascalCase` & `camelCase`.
* In NalchiSharp, functions in [`nalchi::socket_extension`](https://nalchi-net.github.io/nalchi/classnalchi_1_1socket__extensions.html) is implemented as the extension methods of `GnsSharp.ISteamNetworkingSockets`

## Integrate

There are 8 different nuget packages available for different backends & platforms.

```powershell
dotnet add package NalchiSharp.Gns.Win64 --prerelease           # Open source GNS for Windows 64-bit
dotnet add package NalchiSharp.Gns.Win32 --prerelease           # Open source GNS for Windows 32-bit
dotnet add package NalchiSharp.Gns.Posix64 --prerelease         # Open source GNS for POSIX 64-bit
dotnet add package NalchiSharp.Gns.Posix32 --prerelease         # Open source GNS for POSIX 32-bit
dotnet add package NalchiSharp.Steamworks.Win64 --prerelease    # Steamworks SDK for Windows 64-bit
dotnet add package NalchiSharp.Steamworks.Win32 --prerelease    # Steamworks SDK for Windows 32-bit
dotnet add package NalchiSharp.Steamworks.Posix64 --prerelease  # Steamworks SDK for POSIX 64-bit
dotnet add package NalchiSharp.Steamworks.Posix32 --prerelease  # Steamworks SDK for POSIX 32-bit
```

This is because GNS uses different struct pack size for each platform, and `size_t` is different between 64/32 bits.

## Where's the native libraries?

I don't provide them here, for the same reason as in [GnsSharp](https://github.com/nalchi-net/GnsSharp?tab=readme-ov-file#wheres-the-native-libraries).

So, bring your own native dll/dylib/so files:
* Build the [GameNetworkingSockets](https://github.com/ValveSoftware/GameNetworkingSockets) on your own, or download the Steamworks SDK from the [Steamworks partner site](https://partner.steamgames.com/).
* Also, build the C++ version of [nalchi](https://github.com/nalchi-net/nalchi) dynamically and bring that dll/dylib/so file too.

### Can you provide the pre-built native libraries?

You can download the pre-built native libraries in [nalchi](https://github.com/nalchi-net/nalchi) repository.\
Download the one for your platform, that ends with `-Shared`.
