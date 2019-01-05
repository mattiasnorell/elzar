using System;
using System.Collections.Generic;
using Feedbag.DataAccess.Entites;
using Feedbag.Models;

namespace Feedbag.Business.Mappers{
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
            model.Tags = recipe.Tags?.Split(';');

            return model;
        }
    }
}