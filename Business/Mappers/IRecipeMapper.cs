using Elzar.DataAccess.Entites;
using Elzar.Models;

namespace Elzar.Business.Mappers{
    public interface IRecipeMapper{
        RecipeDto ToDto(Recipe recipe);
        Recipe ToDao(UpdateRecipeDto updateRecipeDto);
    }
}