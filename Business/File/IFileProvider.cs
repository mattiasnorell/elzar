using System.IO;

namespace Feedbag.File{
    public interface IFileProvider{
        void Save(Stream stream, string fileName);
    }
}