using System.Collections.Generic;
using Feedbag.Models;

namespace Feedbag.Business.Parser {
    public class RecipeParserResult {
        public string Title{get;set;}
        public string Image{get;set;}
        public string Description{get;set;}
        public string SourceUrl {get;set;}
        public List<IngredientParserResult> Ingredients { get;set;}
        public string[] HowTo {get;set;}
    }
}