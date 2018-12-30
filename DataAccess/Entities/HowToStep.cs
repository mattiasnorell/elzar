using System;
using System.Collections.Generic;

namespace Feedbag.DataAccess.Entites{
    public class HowToStep{
        public int Id {get;set;}
        public int RecipeId {get;set;}
        public string Step{get;set;}
    }
}