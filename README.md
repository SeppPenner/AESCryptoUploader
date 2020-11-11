AESCryptoUploader
====================================

AESCryptoUploader is a project to upload encrypted files to [Google Drive](https://www.google.com/drive/) and [Mega.nz](https://mega.nz/). The project was written and tested in .Net 5.0.

[![Build status](https://ci.appveyor.com/api/projects/status/2lenu7vx01dhonu9?svg=true)](https://ci.appveyor.com/project/SeppPenner/aescryptouploader)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/AESCryptoUploader.svg)](https://github.com/SeppPenner/AESCryptoUploader/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/AESCryptoUploader.svg)](https://github.com/SeppPenner/AESCryptoUploader/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/AESCryptoUploader.svg)](https://github.com/SeppPenner/AESCryptoUploader/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://raw.githubusercontent.com/SeppPenner/AESCryptoUploader/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/AESCryptoUploader/badge.svg)](https://snyk.io/test/github/SeppPenner/AESCryptoUploader)

# Screenshots from the GUI
![Screenshot from the GUI German](https://github.com/SeppPenner/AESCryptoUploader/blob/master/Screenshot_DE.PNG "Screenshot from the GUI German")
![Screenshot from the GUI English](https://github.com/SeppPenner/AESCryptoUploader/blob/master/Screenshot_EN.PNG "Screenshot from the GUI English")

# The configuration file needs to look like this:
```xml
<?xml version="1.0" encoding="utf-8"?>
<Config xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
    <Accounts>
        <Account>
            <UserName>test@test.email</UserName>
            <Password>YourPassword</Password>
            <ClientId></ClientId>
            <Name>Mega</Name>
        </Account>
        <Account>
            <UserName>Test@googlemail.com</UserName>
            <Password>YourPassword</Password>
            <ClientId>something.apps.googleusercontent.com</ClientId>
            <Name>GDrive</Name>
        </Account>
    </Accounts>
</Config>
```

You get the client id for [Google Drive](https://www.google.com/drive/) following https://developers.google.com/drive/api/v3/enable-sdk. For [Mega.nz](https://mega.nz/), this field simply is empty.

Change history
--------------

See the [Changelog](https://github.com/SeppPenner/AESCryptoUploader/blob/master/Changelog.md).