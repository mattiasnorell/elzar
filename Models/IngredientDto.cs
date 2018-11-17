using System.Collections.Generic;

namespace Feedbag.Models {
    public class IngredientDto {
        public int Id {get;set;}
        public int RecepieId {get;set;}
        public string Amount{get;set;}
        public string Unit{get;set;}
        public string Name {get;set;}
    }
}