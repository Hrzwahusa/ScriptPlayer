# .NET 10.0 Upgrade Plan

## ⚠️ Important Notes

**Package Migration Challenges:**
This upgrade includes a few NuGet packages that are not directly compatible with .NET 10.0 and will require investigation:

1. **FMUtils.KeyboardHook** - Used for global keyboard hooks (media keys). Will need alternative solution or P/Invoke implementation.
2. **MediaInfo.Wrapper** - Used for video file metadata reading. Need to check for updated version or find alternative library.

**✅ Good News - Buttplug:**
- **Buttplug 5.0.0** is available and fully compatible with .NET 10.0!
- **ButtplugRustFFI** is no longer needed - version 5.0.0 is a pure .NET implementation without Rust FFI bindings
- Device control functionality is secured for the upgrade

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade ScriptPlayer\ScriptPlayer.csproj
4. Upgrade ScriptPlayer.Shared\ScriptPlayer.Shared.csproj
5. Upgrade ScriptPlayer.VideoSync\ScriptPlayer.VideoSync.csproj
6. Upgrade ScriptPlayer.Cli\ScriptPlayer.Cli.csproj
7. Upgrade ScriptPlayer.Ipc\ScriptPlayer.Ipc.csproj
8. Upgrade ScriptPlayer.Console\ScriptPlayer.Console.csproj
9. Upgrade MK312WifiDotNetLib\MK312WifiLibDotNet.csproj
10. Upgrade ScriptPlayer.HandyAPIv3Playground\ScriptPlayer.HandyAPIv3Playground.csproj
11. Upgrade ScriptPlayer.HandyApi\ScriptPlayer.HandyApi.csproj

## Settings

This section contains settings and data used by execution steps.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                            | Current Version | New Version | Description                                                    |
|:----------------------------------------|:---------------:|:-----------:|:---------------------------------------------------------------|
| Buttplug                                | 2.0.6           | 5.0.0       | Major upgrade - pure .NET implementation, fully compatible     |
| ButtplugRustFFI                         | 2.0.5           | (remove)    | No longer needed - replaced by Buttplug 5.0.0                  |
| FMUtils.KeyboardHook                    | 1.0.140.2145    | ?           | Not compatible - **requires alternative or P/Invoke implementation** |
| MediaInfo.Wrapper                       | 21.9.2          | ?           | Not compatible - **check for newer version or alternative**     |
| Microsoft.Win32.Registry                | 4.7.0           |             | Functionality included in framework reference                  |
| Newtonsoft.Json                         | 13.0.1          | 13.0.4      | NuGet package upgrade recommended                              |
| System.Buffers                          | 4.5.1           |             | Functionality included in framework reference                  |
| System.Memory                           | 4.5.4           |             | Functionality included in framework reference                  |
| System.Numerics.Vectors                 | 4.5.0           |             | Functionality included in framework reference                  |
| System.Runtime.CompilerServices.Unsafe  | 4.5.3           | 6.1.2       | NuGet package upgrade recommended                              |
| System.Security.AccessControl           | 4.7.0           | 6.0.1       | NuGet package upgrade recommended                              |
| System.Security.Principal.Windows       | 4.7.0           |             | Functionality included in framework reference                  |
| System.Threading.Tasks.Dataflow         | 4.11.1          | 10.0.2      | NuGet package upgrade recommended                              |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### ScriptPlayer\ScriptPlayer.csproj modifications

Project properties changes:
  - SDK-style conversion required (currently classic .NET Framework project)
  - Target framework should be changed from `net48` to `net10.0-windows`

NuGet packages changes:
  - FMUtils.KeyboardHook (1.0.140.2145) - **REQUIRES INVESTIGATION**: Package is not compatible with .NET 10.0. Used for global keyboard hooks (media keys). Need to find alternative or implement manual keyboard hook using P/Invoke.
  - MediaInfo.Wrapper (21.9.2) - **REQUIRES INVESTIGATION**: Package is not compatible with .NET 10.0. Used for reading video file metadata. Check if newer version exists or find alternative media info library.  
  - Microsoft.Win32.Registry should be removed (functionality included in framework)
  - Newtonsoft.Json should be updated from `13.0.1` to `13.0.4` (recommended for .NET 10.0)
  - System.Security.AccessControl should be updated from `4.7.0` to `6.0.1` (recommended for .NET 10.0)
  - System.Security.Principal.Windows should be removed (functionality included in framework)

API changes:
  - 3349 binary incompatible APIs require code review (primarily WPF and Windows Forms)
  - WPF APIs require enabling Windows Desktop support via net10.0-windows target

#### ScriptPlayer.Shared\ScriptPlayer.Shared.csproj modifications

Project properties changes:
  - SDK-style conversion required (currently classic .NET Framework project)
  - Target framework should be changed from `net48` to `net10.0-windows`

NuGet packages changes:
  - Buttplug should be updated from `2.0.6` to `5.0.0` (pure .NET implementation, fully compatible with .NET 10.0)
  - ButtplugRustFFI should be removed (no longer needed with Buttplug 5.0.0)
  - Microsoft.Win32.Registry should be removed (functionality included in framework)
  - Newtonsoft.Json should be updated from `13.0.1` to `13.0.4` (recommended for .NET 10.0)
  - System.Buffers should be removed (functionality included in framework)
  - System.Memory should be removed (functionality included in framework)
  - System.Numerics.Vectors should be removed (functionality included in framework)
  - System.Runtime.CompilerServices.Unsafe should be updated from `4.5.3` to `6.1.2` (recommended for .NET 10.0)
  - System.Security.AccessControl should be updated from `4.7.0` to `6.0.1` (recommended for .NET 10.0)
  - System.Security.Principal.Windows should be removed (functionality included in framework)
  - System.Threading.Tasks.Dataflow should be updated from `4.11.1` to `10.0.2` (recommended for .NET 10.0)

API changes:
  - 5123 binary incompatible APIs require code review (primarily WPF)
  - WPF APIs require enabling Windows Desktop support via net10.0-windows target

#### ScriptPlayer.VideoSync\ScriptPlayer.VideoSync.csproj modifications

Project properties changes:
  - SDK-style conversion required (currently classic .NET Framework project)
  - Target framework should be changed from `net48` to `net10.0-windows`

API changes:
  - 1090 binary incompatible APIs require code review (primarily WPF)
  - WPF APIs require enabling Windows Desktop support via net10.0-windows target

#### ScriptPlayer.Cli\ScriptPlayer.Cli.csproj modifications

Project properties changes:
  - SDK-style conversion required (currently classic .NET Framework project)
  - Target framework should be changed from `net48` to `net10.0-windows`

#### ScriptPlayer.Ipc\ScriptPlayer.Ipc.csproj modifications

Project properties changes:
  - SDK-style conversion required (currently classic .NET Framework project)
  - Target framework should be changed from `net48` to `net10.0`

#### ScriptPlayer.Console\ScriptPlayer.Console.csproj modifications

Project properties changes:
  - SDK-style conversion required (currently classic .NET Framework project)
  - Target framework should be changed from `net48` to `net10.0`

#### MK312WifiDotNetLib\MK312WifiLibDotNet.csproj modifications

Project properties changes:
  - Target framework should be changed from `net48` to `net10.0`

API changes:
  - 30 source incompatible APIs require re-compilation

#### ScriptPlayer.HandyAPIv3Playground\ScriptPlayer.HandyAPIv3Playground.csproj modifications

Project properties changes:
  - SDK-style conversion required (currently classic .NET Framework project)
  - Target framework should be changed from `net48` to `net10.0-windows`

NuGet packages changes:
  - Newtonsoft.Json should be updated from `13.0.1` to `13.0.4` (recommended for .NET 10.0)

API changes:
  - 15 binary incompatible APIs require code review
  - WPF APIs require enabling Windows Desktop support via net10.0-windows target

#### ScriptPlayer.HandyApi\ScriptPlayer.HandyApi.csproj modifications

Project properties changes:
  - SDK-style conversion required (currently classic .NET Framework project)
  - Target framework should be changed from `net48` to `net10.0-windows`

NuGet packages changes:
  - Newtonsoft.Json should be updated from `13.0.1` to `13.0.4` (recommended for .NET 10.0)

API changes:
  - 37 binary incompatible APIs require code review
  - WPF APIs require enabling Windows Desktop support via net10.0-windows target
