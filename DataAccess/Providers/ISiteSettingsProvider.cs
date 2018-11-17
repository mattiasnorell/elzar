using Feedbag.DataAccess.Entites;

namespace Feedbag.DataAccess.Providers{
    public interface ISiteSettingsProvider{
        SourceSite GetSourceSiteSettings(string id);
    }
}