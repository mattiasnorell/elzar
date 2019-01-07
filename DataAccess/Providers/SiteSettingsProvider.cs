using System.Collections.Generic;
using System.Linq;
using Elzar.DataAccess.Entites;
using System.IO;
using Newtonsoft.Json;

namespace Elzar.DataAccess.Providers{

    // HARDCODED AS OF NOW. DATABASE COMING IN THE FUTURE
    public class SiteSettingsProvider : ISiteSettingsProvider
    {
        private List<SourceSite> Load(){
            var file = System.IO.File.OpenRead("./SiteSettings.json");
            using(var stream = new StreamReader(file)){
                var content = stream.ReadToEnd();
                return JsonConvert.DeserializeObject<List<SourceSite>>(content);
            }
        }
        public SourceSite GetSourceSiteSettings(string url)
        {
            var settings = Load();
            return settings.Where(e => e.Url == url).FirstOrDefault();
        }
    }
}