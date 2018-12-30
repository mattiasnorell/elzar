using Feedbag.DataAccess.Entites;

namespace Feedbag.Business.Parser{
    public interface IRecipeParser{
        RecipeParserResult Parse(string html, SourceSite settings);
    }
}