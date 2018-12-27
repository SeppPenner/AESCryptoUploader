using System.Collections.Generic;
using System.IO;
using Interfaces;
using Models;

namespace Implementation
{
    public class FileWriter : IFileWriter
    {
        public void WriteToFile(UploadItem item)
        {
            WriteToFile(item.DocuFile, GenerateLines(item));
        }

        private IEnumerable<string> GenerateLines(UploadItem item)
        {
            return new List<string>
            {
                Path.GetFileNameWithoutExtension(item.FileName),
                Path.GetFileNameWithoutExtension(item.NewFileName),
                "[color=#FF0000]Passwort:[/color] [quote]" + item.Password + "[/quote]",
                "[color=#FF0000]Links:[/color]",
                "[list]",
                "[*]Download: [url]" + item.MegaLink + "[/url]",
                "[*]Backup: [url]" + item.GDriveLink + "[/url]",
                "[*]Backup2: [url]" + item.FilehorstLink + "[/url]",
                "[/list]"
            };
        }

        private void WriteToFile(string outputFile, IEnumerable<string> lines)
        {
            using (var writer = new StreamWriter(outputFile))
            {
                foreach (var line in lines)
                    writer.WriteLine(line);
            }
        }
    }
}