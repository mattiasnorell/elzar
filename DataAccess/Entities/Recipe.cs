using System;
using System.Collections.Generic;

namespace Feedbag.DataAccess.Entites{
    public class Recipe{
        public int Id {get;set;}
        public string Title{get;set;}
        public string Image{get;set;}
        public string Description{get;set;}
        public string SourceUrl {get;set;}
        public string[] HowTo{get;set;}
        public List<Ingredient> Ingredients { get;set;}
        public DateTime CreatedAtUtc {get;set;}
        public DateTime UpdatedAtUtc {get;set;}
    }
}