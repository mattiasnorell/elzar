using System;
using System.Collections.Generic;
using Feedbag.DataAccess.Entites;
using Feedbag.Models;

namespace Feedbag.Business.Mappers{
    public class RecipeMapper : IRecipeMapper
    {
        public Recipe FromDto(RecipeDto recipe)
        {
            var model = new Recipe();
            model.Id = recipe.Id;
            model.Title = recipe.Title;
            model.Image = recipe.Image;
            model.Description = recipe.Description;

            return model;
        }

        public RecipeDto ToDto(Recipe recipe)
        {
            var model = new RecipeDto();
            model.Title = recipe.Title;
            model.Image = recipe.Image;
            model.Description = recipe.Description;
            model.HowTo = recipe.HowTo;
            model.Ingredients = new List<IngredientDto>();
            foreach(var Ingredient in recipe.Ingredients){
                model.Ingredients.Add(new IngredientDto(){
                    Amount = Ingredient.Amount,
                    Unit = Ingredient.Unit,
                    Name = Ingredient.Name
                });
            }

            return model;
        }
    }
}