using Feedbag.DataAccess.Entites;

namespace Feedbag.Business.Parser{
    public interface IRecipeParser{
        Recipe Parse(string html, SourceSite settings);
    }
}