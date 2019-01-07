using System.IO;
using System.Net;
using System.Text;

namespace Elzar.Business.Scraper{
    public class Scraper:IScraper{
        public string Run(string path){
            using(var client = new WebClient()){
                var result = client.OpenRead(path);

                using(var reader = new StreamReader(result, Encoding.UTF8)){
                    return reader.ReadToEnd();
                }
            }
        }
    }
}