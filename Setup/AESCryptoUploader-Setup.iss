; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "AESCryptoUploader"
#define MyAppVersion "1.0.1.0"
#define MyAppPublisher "H�mmer Electronics"
#define MyAppURL "softwareload24.de.tl"
#define MyAppExeName "AESCryptoUploader.exe"
#define MyCopyRight "Copyright (�) H�mmer Electronics"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{356E8042-A10A-448E-9004-FBD1CF14BF8A}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
VersionInfoVersion={#MyAppVersion}
VersionInfoProductVersion={#MyAppVersion}
AppCopyright={#MyCopyRight}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={commonpf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=..\src\AESCryptoUploader\License.txt
OutputDir=..\Setup
OutputBaseFilename=AESCryptoUploader-Setup
SetupIconFile=..\src\AESCryptoUploader\AES.ico
UninstallDisplayIcon=..\src\AESCryptoUploader\AES.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\AESCryptoUploader.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\AESCryptoUploader.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Config.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Google.Apis.Auth.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Google.Apis.Auth.PlatformServices.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Google.Apis.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Google.Apis.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Google.Apis.Drive.v3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Languages.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\languages\*"; DestDir: "{app}\languages\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\MegaApiClient.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Serilog.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\Serilog.Sinks.File.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\SharpAESCrypt.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\AESCryptoUploader\bin\Release\net5.0-windows\License.txt"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

