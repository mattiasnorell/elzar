using System.IO;

namespace Feedbag.Pdf{
    public interface IPdfGenerator{
        Stream Generate(string path);
    }
}