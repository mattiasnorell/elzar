using System;
using System.Collections.Generic;
using Feedbag.Models;

namespace Feedbag.Business.Repositories{
    public class RecipeRepository : IRecipeRepository
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public RecipeDto Get(Guid id)
        {
            return new RecipeDto(){ Title = "Test 1" };
        }

        public List<RecipeDto> GetAll()
        {
            var recipes = new List<RecipeDto>(){
                new RecipeDto(){
                    Title = "Test 1"
                },
                new RecipeDto(){
                    Title = "Test 2"
                },
                new RecipeDto(){
                    Title = "Test 3"
                }
            };

            return recipes;
        }

        public void Save(UpdateRecipeDto recipe)
        {
            throw new NotImplementedException();
        }
    }
}