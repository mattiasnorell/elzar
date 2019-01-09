using System.IO;

namespace Elzar.FileHandlers{
    public interface IFileHandler{
        void Save(Stream stream, string fileName);
        void Download(string url, string filename);
        void Delete(int id);
    }
}