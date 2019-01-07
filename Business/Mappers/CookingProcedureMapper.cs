using Feedbag.DataAccess.Entites;
using Feedbag.Models;

namespace Feedbag.Business.Mappers
{
    public class CookingProcedureMapper : ICookingProcedureMapper
    {
        public CookingProcedureStep FromDto(CookingProcedureDto recipe)
        {
            var model = new CookingProcedureStep();
            model.Id = recipe.Id;
            model.RecipeId = recipe.RecipeId;
            model.Step = recipe.Step;

            return model;
        }

        public CookingProcedureDto ToDto(CookingProcedureStep recipe)
        {
            var model = new CookingProcedureDto();
            model.Id = recipe.Id;
            model.RecipeId = recipe.RecipeId;
            model.Step = recipe.Step;

            return model;
        }
    }
}