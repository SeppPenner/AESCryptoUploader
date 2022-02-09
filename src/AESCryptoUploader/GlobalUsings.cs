#pragma warning disable IDE0065 // Die global using-Anweisung wurde falsch platziert.
global using System.Collections;
global using System.ComponentModel;
global using System.Security.Cryptography;
global using System.Text;
global using System.Xml.Linq;
global using System.Xml.Serialization;

global using AESCryptoUploader.Events;
global using AESCryptoUploader.Implementation;
global using AESCryptoUploader.Interfaces;
global using AESCryptoUploader.Models;
global using AESCryptoUploader.UiThreadInvoke;

global using CG.Web.MegaApiClient;

global using Google.Apis.Auth.OAuth2;
global using Google.Apis.Drive.v3;
global using Google.Apis.Drive.v3.Data;
global using Google.Apis.Http;
global using Google.Apis.Services;
global using Google.Apis.Upload;
global using Google.Apis.Util.Store;

global using Languages.Implementation;
global using Languages.Interfaces;

global using Microsoft.Win32;

global using Serilog;

global using GoogleFile = Google.Apis.Drive.v3.Data.File;
global using IOFile = System.IO.File;
global using AESCrypt = SharpAESCrypt.SharpAESCrypt;
#pragma warning restore IDE0065 // Die global using-Anweisung wurde falsch platziert.