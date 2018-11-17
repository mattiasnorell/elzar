using System.Collections.Generic;

namespace Feedbag.Models {
    public class RecipeDto {
        public string Title{get;set;}
        public string Image{get;set;}
        public string Description{get;set;}
        public string SourceUrl {get;set;}
        public List<IngredientDto> Ingredients { get;set;}
        public string[] HowTo {get;set;}
        public string[] Tags { get;set;}
    }
}