using Elzar.DataAccess.Entites;

namespace Elzar.Business.Parser{
    public interface IRecipeParser{
        RecipeParserResult Parse(string html, SourceSite settings);
    }
}