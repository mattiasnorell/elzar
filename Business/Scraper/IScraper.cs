using System.IO;
using System.Net;
using System.Text;

namespace Elzar.Business.Scraper{
    public interface IScraper{
        string Run(string path);
    }
}