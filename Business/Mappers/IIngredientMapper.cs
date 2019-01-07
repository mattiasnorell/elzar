using Elzar.Business.Parser;
using Elzar.DataAccess.Entites;
using Elzar.Models;

namespace Elzar.Business.Mappers{
    public interface IIngredientMapper{
        IngredientDto ToDto(Ingredient recipe);
        Ingredient FromDto(UpdateIngredientDto recipe);
        IngredientDto ToDto(IngredientParserResult ingredient);
    }
}