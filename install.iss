#define MyAppVersion "0.15.0.1"

[Setup]
AppName=Sentinel
AppVersion={#MyAppVersion}
DefaultDirName={pf}\Sentinel
DefaultGroupName=Sentinel
SetupIconFile=Sentinel/Sentinel.ico
OutputBaseFilename=Sentinel_{#MyAppVersion}_Setup
OutputDir=.
Compression=lzma
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes

[Files]
Source: "publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Sentinel"; Filename: "{app}\Sentinel.exe"
Name: "{group}\Uninstall Sentinel"; Filename: "{uninstallexe}"
Name: "{commondesktop}\Sentinel"; Filename: "{app}\Sentinel.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Create a desktop icon"; GroupDescription: "Additional icons"

[Run]
Filename: "{app}\Sentinel.exe"; Description: "Launch Sentinel"; Flags: nowait postinstall skipifsilent

