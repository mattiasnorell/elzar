using System;

namespace Feedbag.DataAccess.Entites{
    public class Ingredient{
        public int Id {get;set;}
        public int RecepieId {get;set;}
        public string Amount{get;set;}
        public string Unit{get;set;}
        public string Name {get;set;}
    }
}