using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeForever
{
    internal class DownloadFFMPEG
    {
        public async Task CheckFFMPEG()
        {
            string downloadUrl = "https://github.com/GyanD/codexffmpeg/releases/download/6.0/ffmpeg-6.0-essentials_build.zip";
            string downloadPath = "ffmpeg-6.0-essentials_build.zip";
            string extractFolder = "ffmpeg";   
           
            using (HttpClient client = new HttpClient())
            {
                byte[] fileData = await client.GetByteArrayAsync(downloadUrl);
                File.WriteAllBytes(downloadPath, fileData);
            }

            using (ZipArchive archive = ZipFile.OpenRead(downloadPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith("ffmpeg.exe", StringComparison.OrdinalIgnoreCase))
                    {
                        entry.ExtractToFile(Path.Combine(extractFolder, "ffmpeg.exe"), true);
                        break; 
                    }
                }
            }

            File.Delete(downloadPath);                           
            
        }
    }
}
