using System.IO;

namespace Feedbag.File{
    public class FileProvider: IFileProvider{
        public void Save(Stream stream, string fileName){

            if(!Directory.Exists("c:\\temp\\feedbag\\")){
                Directory.CreateDirectory("c:\\temp\\feedbag\\");
            }

            using(var fileStream = new FileStream($"c:\\temp\\feedbag\\{fileName}.pdf", FileMode.Create, FileAccess.Write)){
                stream.CopyTo(fileStream);
            }
        }
    }
}