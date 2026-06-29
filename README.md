# Omniconvert
C# file conversion software.

The purpose of this program is to be able to quickly convert file formats through the Windows context menu.

## Build / Install:

- For CLI version:
`dotnet publish -c Release` 
    
- For silent version used with context menus:
`dotnet publish -c Silent`

- Add Omniconvert to Windows context menu:
`./omniconvert.exe --install`

- Remove Omniconvert from Windows context menu:
`./omniconvert.exe --uninstall`

## Dependencies:

- [.NET Framework](https://dotnet.microsoft.com/en-us/download)
- [FFmpeg](https://www.ffmpeg.org/download.html)
