using System.Collections.Generic;

namespace Elzar.Models {
    public class RecipeDto {
        public string Title{get;set;}
        public string Image{get;set;}
        public string Description{get;set;}
        public string SourceUrl {get;set;}
        public List<IngredientDto> Ingredients { get;set;}
        public string[] CookingProcedureSteps {get;set;}
        public string[] Tags { get;set;}
        public int Id { get; set; }
    }
}