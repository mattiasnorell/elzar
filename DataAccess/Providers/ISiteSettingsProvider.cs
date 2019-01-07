using Elzar.DataAccess.Entites;

namespace Elzar.DataAccess.Providers{
    public interface ISiteSettingsProvider{
        SourceSite GetSourceSiteSettings(string id);
    }
}