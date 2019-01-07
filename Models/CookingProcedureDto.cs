using System.Collections.Generic;

namespace Feedbag.Models {
    public class CookingProcedureDto {
        public int Id {get;set;}
        public int RecipeId {get;set;}
        public string Step {get;set;}
    }
}