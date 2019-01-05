using System;

namespace Feedbag.DataAccess.Entites{
    public class SourceSite{
        public Guid Id {get;set;}
        public string Url{get;set;}
        public string TitleElement{get;set;}
        public SourceSiteImage ImageElement{get;set;}
        public string DescriptionElement{get;set;}
        public string IngredientsElement{get;set;}
        public string[] HowToElement{get;set;}
        public string TagsElement { get;set;}

        public Boolean UseBruteForce { get;set;}
    }
}