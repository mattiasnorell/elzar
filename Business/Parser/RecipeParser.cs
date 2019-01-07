using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Elzar.DataAccess.Entites;
using HtmlAgilityPack;

namespace Elzar.Business.Parser{
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
            recipe.HowTo = GetHowTo(htmlDocument, settings.HowToElement, settings.UseBruteForce);
            recipe.Ingredients = GetIngredients(htmlDocument, settings.IngredientsElement, settings.UseBruteForce);
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

        private string[] GetHowTo(HtmlDocument doc, string[] selectors, bool bruteForce = false)
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

                break;
            }

            return model.ToArray();
        }

        private string GetImage(HtmlDocument doc, SourceSiteImage selector)
        {
            var image = doc.DocumentNode.SelectNodes(selector.Path)?.FirstOrDefault();

            if(image == null){
                return null;
            }

            return image.Attributes[selector.Attribute].Value;
        }

        private List<IngredientParserResult> GetIngredients(HtmlDocument doc, string selector, bool bruteForce = false)
        {
            var model = new List<IngredientParserResult>();
            var items = doc.DocumentNode.SelectNodes(selector);
            var ingredientLines = new List<string>();

            if(bruteForce){
                
                foreach(var item in items){
                    var ingredientRaw = StripTagsRegex(item.InnerText);

                    if(!this.ingredientParser.IsIngredientList(ingredientRaw)){
                        continue;
                    }

                    var ingredientRows = ingredientRaw.Split("\n").Select(x => StripNonText(x));
                    ingredientLines.AddRange(ingredientRows);
                }

            }else{
                foreach(var item in items){
                    var ingredientRaw = StripNonText(item.InnerText);
                    ingredientLines.Add(ingredientRaw);
                }
            }

            foreach(var line in ingredientLines){
                var ingredient = this.ingredientParser.Parse(line);

                if(ingredient == null){
                    continue;
                }
                
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