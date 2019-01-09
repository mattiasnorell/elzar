using System;
using System.IO;
using System.Net;
using Elzar.Models;
using Microsoft.Extensions.Options;

namespace Elzar.FileHandlers{
    public class FileHandler: IFileHandler{
        private readonly IOptions<AppSettings> settings;

        public FileHandler(IOptions<AppSettings> settings){
            this.settings = settings;
        }   
        public void Save(Stream stream, string fileName){

            if(!Directory.Exists("c:\\temp\\feedbag\\")){
                Directory.CreateDirectory("c:\\temp\\feedbag\\");
            }

            using(var fileStream = new FileStream($"c:\\temp\\feedbag\\{fileName}.pdf", FileMode.Create, FileAccess.Write)){
                stream.CopyTo(fileStream);
            }
        }

        public void Download(string url, string filename)
        {
            using (var webClient = new WebClient())
            {
                var filePath = Path.Combine($"{this.settings.Value.ImagePhysicalPath}", filename);
                webClient.DownloadFileAsync(new Uri(url), filePath);
            }
        }

        public void Delete(int id){
            var fileName = $"{id}.jpg";
            var filePath = Path.Combine($"{this.settings.Value.ImagePhysicalPath}", fileName);

            if(File.Exists(filePath)){
                File.Delete(filePath);
            }
        }
    }
}