using System;
using System.Collections.Generic;
using Feedbag.DataAccess.Entites;
using Feedbag.Models;

namespace Feedbag.Business.Mappers{
    public class HowToMapper : IHowToMapper
    {
        public HowToStep FromDto(HowToStepDto recipe)
        {
            var model = new HowToStep();
            model.Id = recipe.Id;
            model.RecipeId = recipe.RecipeId;
            model.Step = recipe.Step;

            return model;
        }

        public HowToStepDto ToDto(HowToStep recipe)
        {
            var model = new HowToStepDto();
            model.Id = recipe.Id;
            model.RecipeId = recipe.RecipeId;
            model.Step = recipe.Step;

            return model;
        }
    }
}