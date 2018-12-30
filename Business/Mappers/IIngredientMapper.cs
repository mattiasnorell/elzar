using Feedbag.Business.Parser;
using Feedbag.DataAccess.Entites;
using Feedbag.Models;

namespace Feedbag.Business.Mappers{
    public interface IIngredientMapper{
        IngredientDto ToDto(Ingredient recipe);
        Ingredient FromDto(IngredientDto recipe);
        IngredientDto ToDto(IngredientParserResult ingredient);
    }
}