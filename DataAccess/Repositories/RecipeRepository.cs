using System;
using System.Collections.Generic;
using Feedbag.DataAccess.Entites;

namespace Feedbag.DataAccess.Repositories{
    public class RecipeRepository: IRecipeRepository{
        public RecipeRepository(){

        }

        public List<Recipe> GetAll(){
            return new List<Recipe>() {
                new Recipe(){
                    Title = "Recipe title",
                    Image = "https://picsum.photos/200/300",
                    SourceUrl = "http://www.mattiasnorell.com"
                }
            };
        }

        public Recipe Get(Guid id){
            return new Recipe(){
                Title = "Recipe title",
                Image = "https://picsum.photos/200/300",
                SourceUrl = "http://www.mattiasnorell.com"
            };
        }
    }
}