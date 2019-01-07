using Elzar.DataAccess.Entites;

namespace Elzar.Business.Parser{
    public interface IIngredientParser{
        IngredientParserResult Parse(string input);
        bool IsIngredientList(string input);
    }
}