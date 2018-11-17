using Feedbag.DataAccess.Entites;

namespace Feedbag.Business.Parser{
    public interface IIngredientParser{
        Ingredient Parse(string input);
    }
}