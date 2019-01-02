using System;
using System.Collections.Generic;
using Feedbag.Business.Parser;
using Feedbag.DataAccess.Entites;
using Feedbag.Models;

namespace Feedbag.Business.Mappers{
    public class IngredientMapper : IIngredientMapper
    {
        public Ingredient FromDto(IngredientDto ingredient)
        {
            var model = new Ingredient();
            model.Id = ingredient.Id;
            model.RecipeId = ingredient.RecipeId;
            model.Amount = ingredient.Amount;
            model.Unit = ingredient.Unit;
            model.Name = ingredient.Name;

            return model;
        }

        public IngredientDto ToDto(Ingredient ingredient)
        {
            var model = new IngredientDto();
            
            model.Id = ingredient.Id;
            model.Amount = ingredient.Amount;
            model.Unit = ingredient.Unit;
            model.Name = ingredient.Name;

            return model;
        }

        public IngredientDto ToDto(IngredientParserResult ingredient)
        {
            var model = new IngredientDto();
            
            model.Amount = ingredient.Amount;
            model.Unit = ingredient.Unit;
            model.Name = ingredient.Name;

            return model;
        }
    }
}