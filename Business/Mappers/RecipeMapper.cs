using System;
using System.Collections.Generic;
using Elzar.DataAccess.Entites;
using Elzar.Models;

namespace Elzar.Business.Mappers{
    public class RecipeMapper : IRecipeMapper
    {
        public Recipe ToDao(UpdateRecipeDto recipe)
        {
            if(recipe == null){
                return null;
            }
            
            var model = new Recipe();
            model.Id = recipe.Id;
            model.Title = recipe.Title;
            model.Image = recipe.Image;
            model.Description = recipe.Description;
            model.SourceUrl = recipe.SourceUrl;
            model.Tags = recipe.Tags;
            return model;
        }

        public RecipeDto ToDto(Recipe recipe)
        {
            if(recipe == null){
                return null;
            }

            var model = new RecipeDto();
            model.Id = recipe.Id;
            model.Title = recipe.Title;
            model.Image = recipe.Image;
            model.Description = recipe.Description;
            model.SourceUrl = recipe.SourceUrl;
            model.Tags = recipe.Tags?.ToLowerInvariant().Split(';');

            return model;
        }
    }
}