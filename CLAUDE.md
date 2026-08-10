# CLAUDE.md

Guidance for Claude Code when working in this repository.

## What this is

AESCryptoUploader is a Windows Forms desktop application. It encrypts files with AES
(`SharpAESCrypt`), uploads the encrypted files to Mega.nz and Google Drive and writes a BBCode
documentation file next to the encrypted file that contains the password and the download links.

- Solution: `src/AESCryptoUploader.sln`, one project `src/AESCryptoUploader/AESCryptoUploader.csproj`.
- No test project, no `.github` workflows. The AppVeyor badge in the README is the only CI.
- Target framework `net9.0-windows`, `WinExe`, `RuntimeIdentifiers win-x64`.
- UI text comes from `HaemmerElectronics.SeppPenner.Language`, the language files live in
  `src/AESCryptoUploader/languages/*.xml` and are copied to the output folder.
- The accounts are read at startup from `Config.xml` in the working directory. The tracked
  `Config.xml` contains placeholder credentials only.

Layout inside the project:

| Folder | Content |
| --- | --- |
| `Interfaces/` | `IConfigLoader`, `ICustomGDriveService`, `IErrorHandler`, `IFileCryptor`, `IFileUploader`, `IFileWriter`, `IRandomizer` |
| `Implementation/` | the matching implementations |
| `Models/` | `Account`, `Accounts` (`Config`), `UploadItem`, `LinkTriple`, the progress cell types |
| `Events/` | the upload event args |
| `UiThreadInvoke/` | `Control` extension for marshalling to the UI thread |
| `languages/` | `de-DE.xml`, `en-US.xml` |

The three forms are `Main`, `AccountInfos` and `UploadProgress`.

## Build

```powershell
dotnet build src/AESCryptoUploader.sln -c Release
```

`TreatWarningsAsErrors` is on and `NuGetAudit` runs with `NuGetAuditMode all`, so a clean build is
a real statement: any analyzer warning and any vulnerable transitive package breaks the build.
`NoWarn` contains `NU1803` only (HTTP feeds in the machine wide NuGet configuration).

## Code conventions

`src/.editorconfig` is authoritative, the important points:

- CRLF, 4 spaces, UTF-8, no BOM in the source files.
- `this.` qualification for fields, properties, methods and events, enforced as a warning.
- File scoped namespaces, usings inside the namespace, `IDE0005` (unused usings) as a warning.
- Common usings live in `GlobalUsings.cs`, including the aliases `GoogleFile`, `IOFile` and
  `AESCrypt`.
- Every type and every member carries an XML documentation comment, the file header has the
  `Hämmer Electronics` copyright block. New code follows this without exception, otherwise it
  looks foreign in this repository.

The language files use tabs and CRLF and are stored as UTF-8 without BOM. When editing them keep
that exactly, and remember that `ILanguage.GetWord` returns `null` for an unknown key and does not
fall back to another language, so a new key has to be added to `de-DE.xml` **and** `en-US.xml`.

## Known quirks

- **Setup executable is tracked.** `Setup/AESCryptoUploader-Setup.exe` is in the repository
  although `.gitignore` excludes `*.exe`. It only gets updated with `git add -f`.
- **Inno Setup script encoding.** `Setup/AESCryptoUploader-Setup.iss` is stored in the Windows
  code page without a BOM. Inno Setup only treats a script as UTF-8 when the BOM is present,
  otherwise it uses the system code page, so an editor that saves UTF-8 without BOM turns
  `Hämmer Electronics` into `HÃ¤mmer Electronics` in the installer.
- **Window title.** `Main.InitializeCaption` uses `Application.ProductVersion`, which is the
  GitVersion informational version, so the title shows the commit hash on untagged commits.
- **Filehorst upload is a stub.** `FileUploader.UploadToFilehorst` returns an empty string. The
  wait loop in `Main.WaitIfNotFinishedAndFinish` therefore expects `tasks.Count * 2` rows, not
  three per file.
- **Startup order.** `Main.Initialize` calls `InitializeLanguageManager` after
  `InitializeBackgroundWorker`, and `this.language` is only filled by the `OnLanguageChanged`
  event. Everything that needs the language checks for `null` first.
- **Config.xml is not created.** If the file is missing next to the executable,
  `ConfigLoader.LoadConfigFromXmlFile` throws and the error dialog appears at startup.

## Releasing

Version numbers come from GitVersion, driven by lightweight tags on exact commits. Keep this
order, otherwise a prerelease version is burned into the shipped executable:

1. Add the new version to `Changelog.md`.
2. Set `MyAppVersion` in `Setup/AESCryptoUploader-Setup.iss` (keep the encoding and CRLF).
3. Commit.
4. Tag that commit, lightweight: `git tag 1.0.8`.
5. Run `Setup/build-setup-files.bat`. It deletes `bin` and `obj` below `src`, publishes into
   `src/AESCryptoUploader/bin/publish` and removes the `*.pdb` files.
6. Compile the installer:
   `& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" Setup\AESCryptoUploader-Setup.iss`.
7. `git add -f Setup/AESCryptoUploader-Setup.exe` and commit as `Updated setup.`.
8. `git push origin master` and `git push origin <tag>`.

## Git

Never use `git commit --amend`, not even for purely local commits that have not been pushed. Write
a follow up commit instead. Reason: the versions of this repository come from GitVersion out of
tags that sit on exact commits, and rewriting a commit silently moves that ground.

## Writing style

No em dashes and no en dashes in commit messages, code comments, documentation or answers. Use a
normal hyphen, a comma, a colon, brackets or a separate sentence instead. German texts use real
umlauts, never transliterations.
