namespace Elzar.DataAccess.Entites
{
    public class Recipe{
        public int Id {get;set;}
        public string Title{get;set;}
        public string Image{get;set;}
        public string Description{get;set;}
        public string SourceUrl {get;set;}
        public string Tags {get;set;}
        public string CreatedAtUtc {get;set;}
        public string UpdatedAtUtc {get;set;}
    }
}