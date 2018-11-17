using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Feedbag.DataAccess.Entites;
using HtmlAgilityPack;

namespace Feedbag.Business.Parser{
    public class RecipeParser : IRecipeParser
    {
        private readonly IIngredientParser ingredientParser;

        public RecipeParser(IIngredientParser ingredientParser)
        {
            this.ingredientParser = ingredientParser;
        }

        public Recipe Parse(string html, SourceSite settings){
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var recipe = new Recipe();
            recipe.Title = GetHeadline(htmlDocument, settings.TitleElement);
            recipe.Image = GetImage(htmlDocument, settings.ImageElement);
            recipe.Description = GetDescription(htmlDocument, settings.DescriptionElement);
            recipe.HowTo = GetHowTo(htmlDocument, settings.HowToElement);
            recipe.Ingredients = GetIngredients(htmlDocument, settings.IngredientsElement);

            return recipe;
        }

        private string GetDescription(HtmlDocument doc, string selector)
        {
            if(selector == null){
                return  null;
            }
            
            return StripNonText(doc.DocumentNode.SelectNodes(selector).First().InnerText);
        }

        private string GetHeadline(HtmlDocument doc, string selector)
        {
            return doc.DocumentNode.Descendants(selector).Select(x => x.InnerText).First();
        }

        private string[] GetHowTo(HtmlDocument doc, string[] selectors)
        {
            var model = new List<string>();
            
            foreach(var selector in selectors){
                var steps = doc.DocumentNode.SelectNodes(selector);

                if(steps == null){
                    continue;
                }

                foreach(var step in steps){
                    model.Add(step.InnerText);
                }
                }

            return model.ToArray();
        }

        private string GetImage(HtmlDocument doc, SourceSiteImage selector)
        {
            var image = doc.DocumentNode.SelectNodes(selector.Path).FirstOrDefault();

            if(image == null){
                return null;
            }

            return image.Attributes[selector.Attribute].Value;
        }

        private List<Ingredient> GetIngredients(HtmlDocument doc, string selector)
        {
            var model = new List<Ingredient>();
            var items = doc.DocumentNode.SelectNodes(selector);

            foreach(var item in items){
                var ingredientRaw = StripNonText(item.InnerText);
                var ingredient = this.ingredientParser.Parse(ingredientRaw);
                model.Add(ingredient);
            }

            return model;
        }

        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        private string StripNonText(string input){
            if(string.IsNullOrEmpty(input)){
                return null;
            }

            var replacedInput = input.Replace("\t","").Replace("\r","").Replace("\n","").Trim();

            return StripTagsRegex(replacedInput);
        }

        private string GetBackgroundImage(string styles){
            foreach(var style in styles.Split(';')){
                var styleSplit = style.Split(':');

                if(styleSplit[0] == "background-image"){
                   
                    var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    foreach(Match m in linkParser.Matches(styleSplit[1])){
                        return m.Value;
                    };
                } 
            }

            return null;
        }
    }

}