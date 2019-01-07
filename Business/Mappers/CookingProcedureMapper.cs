using Elzar.DataAccess.Entites;
using Elzar.Models;

namespace Elzar.Business.Mappers
{
    public class CookingProcedureMapper : ICookingProcedureMapper
    {
        public CookingProcedureStep FromDto(CookingProcedureDto step)
        {
            if(step == null){
                return null;
            }

            var model = new CookingProcedureStep();
            model.Id = step.Id;
            model.RecipeId = step.RecipeId;
            model.Step = step.Step;

            return model;
        }

        public CookingProcedureDto ToDto(CookingProcedureStep step)
        {
            if(step == null){
                return null;
            }

            var model = new CookingProcedureDto();
            model.Id = step.Id;
            model.RecipeId = step.RecipeId;
            model.Step = step.Step;

            return model;
        }
    }
}