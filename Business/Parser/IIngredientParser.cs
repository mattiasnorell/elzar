using Feedbag.DataAccess.Entites;

namespace Feedbag.Business.Parser{
    public interface IIngredientParser{
        IngredientParserResult Parse(string input);
    }
}