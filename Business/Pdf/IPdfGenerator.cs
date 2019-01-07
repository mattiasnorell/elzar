using System.IO;

namespace Elzar.Pdf{
    public interface IPdfGenerator{
        Stream Generate(string path);
    }
}