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

        public RecipeParserResult Parse(string html, SourceSite settings){
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var recipe = new RecipeParserResult();
            recipe.Title = GetHeadline(htmlDocument, settings.TitleElement);
            recipe.Image = GetImage(htmlDocument, settings.ImageElement);
            recipe.Description = GetDescription(htmlDocument, settings.DescriptionElement);
            recipe.HowTo = GetHowTo(htmlDocument, settings.HowToElement);
            recipe.Ingredients = GetIngredients(htmlDocument, settings.IngredientsElement);
            recipe.Tags = GetTags(htmlDocument, settings.TagsElement);

            return recipe;
        }
        private string[] GetTags(HtmlDocument doc, string selector)
        {
            if(selector == null){
                return  null;
            }
            
            var model = new List<string>();
            
            var tags = doc.DocumentNode.SelectNodes(selector);

            if(tags == null){
                return null;
            }

            foreach(var tag in tags){
                model.Add(tag.InnerText);
            }
            
            return model.ToArray();
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

        private List<IngredientParserResult> GetIngredients(HtmlDocument doc, string selector)
        {
            var model = new List<IngredientParserResult>();
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
    }

}