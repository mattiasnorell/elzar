using System.IO;
using SelectPdf;

namespace Feedbag.Pdf{
    public class SelectPdfGenerator : IPdfGenerator
    {
        public Stream Generate(string path)
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;
            PdfDocument doc = converter.ConvertUrl(path);
            var stream = new MemoryStream(doc.Save());
            doc.Close();
            return stream;
        }
    }
}