using System.IO;

namespace Elzar.File{
    public interface IFileProvider{
        void Save(Stream stream, string fileName);
    }
}