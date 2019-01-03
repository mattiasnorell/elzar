using Feedbag.DataAccess.Entites;
using Feedbag.Models;

namespace Feedbag.Business.Mappers{
    public interface IRecipeMapper{
        RecipeDto ToDto(Recipe recipe);
        Recipe ToDao(UpdateRecipeDto updateRecipeDto);
    }
}