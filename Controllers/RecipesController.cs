using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Feedbag.Business.Parser;
using Feedbag.DataAccess.Providers;
using Feedbag.Business.Scraper;
using Feedbag.DataAccess.Entites;
using Feedbag.Models;
using Feedbag.Pdf;
using Microsoft.AspNetCore.Mvc;

namespace Feedbag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IPdfGenerator pdfGenerator;
        private readonly IScraper scraper;
        private readonly IRecipeParser RecipeParser;
        private readonly ISiteSettingsProvider siteSettingsProvider;

        public RecipesController(IPdfGenerator pdfGenerator, IScraper scraper, IRecipeParser RecipeParser, ISiteSettingsProvider siteSettingsProvider){
            this.pdfGenerator = pdfGenerator;
            this.scraper = scraper;
            this.RecipeParser = RecipeParser;
            this.siteSettingsProvider = siteSettingsProvider;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<RecipeDto>> Get()
        {
            var Recipes = new List<RecipeDto>(){
                new RecipeDto(){
                    Title = "Test 1"
                },
                new RecipeDto(){
                    Title = "Test 2"
                },
                new RecipeDto(){
                    Title = "Test 3"
                }
            };

            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<RecipeDto> Get(string id)
        {
          
                
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] CreateRecipe model)
        {
            if(model == null || string.IsNullOrEmpty(model.Url)){
                return BadRequest("No url");
            }

            var fileName = Guid.NewGuid();
            var uri = new Uri(model.Url);
            var settings = this.siteSettingsProvider.GetSourceSiteSettings(uri.Host);
    
            if(settings == null){
                return BadRequest("No settings found for that site");
            }

            var html = this.scraper.Run(uri.OriginalString);
            
            var parsedRecipe = this.RecipeParser.Parse(html, settings);
            //var pdfStream = this.pdfGenerator.Generate(url);

            var Recipe = new RecipeDto();
            Recipe.Title = parsedRecipe.Title;
            Recipe.Image = parsedRecipe.Image;
            Recipe.Description = parsedRecipe.Description;
            Recipe.HowTo = parsedRecipe.HowTo;
            Recipe.Ingredients = new List<IngredientDto>();
            foreach(var Ingredient in parsedRecipe.Ingredients){
                Recipe.Ingredients.Add(new IngredientDto(){
                    Amount = Ingredient.Amount,
                    Unit = Ingredient.Unit,
                    Name = Ingredient.Name
                });
            }
            
            Recipe.SourceUrl = model.Url;;
                
            return Ok(Recipe);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
