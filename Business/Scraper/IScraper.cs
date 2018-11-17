using System.IO;
using System.Net;
using System.Text;

namespace Feedbag.Business.Scraper{
    public interface IScraper{
        string Run(string path);
    }
}