using System.Collections.Generic;

namespace Elzar.Models {
    public class UpdateRecipeDto {

        public int Id { get; set; }
        public string Title{get;set;}
        public string Image{get;set;}
        public string Description{get;set;}
        public string SourceUrl {get;set;}
        public List<IngredientDto> Ingredients { get;set;}
        public string[] HowTo {get;set;}
        public string Tags { get;set;}
        
    }
}