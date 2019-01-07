using System;
using System.Collections.Generic;
using Elzar.Business.Parser;
using Elzar.DataAccess.Entites;
using Elzar.Models;

namespace Elzar.Business.Mappers{
    public class IngredientMapper : IIngredientMapper
    {
        public Ingredient FromDto(UpdateIngredientDto ingredient)
        {

            if(ingredient == null){
                return null;
            }

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
            if(ingredient == null){
                return null;
            }

            var model = new IngredientDto();
            model.Id = ingredient.Id;
            model.Amount = ingredient.Amount;
            model.Unit = ingredient.Unit;
            model.Name = ingredient.Name;

            return model;
        }

        public IngredientDto ToDto(IngredientParserResult ingredient)
        {
            if(ingredient == null){
                return null;
            }
            
            var model = new IngredientDto();
            
            model.Amount = ingredient.Amount;
            model.Unit = ingredient.Unit;
            model.Name = ingredient.Name;

            return model;
        }
    }
}