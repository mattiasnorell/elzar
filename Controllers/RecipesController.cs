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
using Feedbag.Business.Providers;
using Feedbag.Models;
using Feedbag.Pdf;
using Microsoft.AspNetCore.Mvc;
using Feedbag.Business.Mappers;

namespace Feedbag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IHowToProvider howToProvider;
        private readonly IIngredientProvider ingredientProvider;
        private readonly IIngredientMapper ingredientMapper;
        private readonly IRecipeProvider recipeProvider;
        private readonly IRecipeMapper recipeMapper;
        private readonly IPdfGenerator pdfGenerator;
        private readonly IScraper scraper;
        private readonly IRecipeParser RecipeParser;
        private readonly ISiteSettingsProvider siteSettingsProvider;

        public RecipesController(
            IHowToProvider howToProvider,
            IIngredientProvider ingredientProvider,
            IIngredientMapper ingredientMapper, 
            IRecipeProvider recipeProvider, 
            IRecipeMapper recipeMapper, 
            IPdfGenerator pdfGenerator,
            IScraper scraper, 
            IRecipeParser RecipeParser, 
            ISiteSettingsProvider siteSettingsProvider
        ){
            this.howToProvider = howToProvider;
            this.ingredientProvider = ingredientProvider;
            this.ingredientMapper = ingredientMapper;
            this.recipeProvider = recipeProvider;
            this.recipeMapper = recipeMapper;
            this.pdfGenerator = pdfGenerator;
            this.scraper = scraper;
            this.RecipeParser = RecipeParser;
            this.siteSettingsProvider = siteSettingsProvider;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetAsync()
        {
            var recipes = await this.recipeProvider.GetAll();
            var returnValue = new List<RecipeDto>();

            foreach(var recipe in recipes){
                var ingredients = await this.ingredientProvider.GetAllByRecipeId(recipe.Id);
                var howTos = await this.howToProvider.GetAllByRecipeId(recipe.Id);
                
                var mappedRecipe = new RecipeDto();

                mappedRecipe.Id = recipe.Id;
                mappedRecipe.Title = recipe.Title;
                mappedRecipe.Description = recipe.Description;
                mappedRecipe.Image = recipe.Image;
                mappedRecipe.SourceUrl = recipe.SourceUrl;
                mappedRecipe.Ingredients = ingredients?.ToList();
                mappedRecipe.HowTo = howTos?.Select(x => x.Step).ToArray();
                
                returnValue.Add(mappedRecipe);
            }
            
            return Ok(returnValue);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetAsync(int id)
        {
            var recipe = await this.recipeProvider.Get(id);

            if(recipe == null){
                return NotFound();
            }

            var ingredients = await this.ingredientProvider.GetAllByRecipeId(recipe.Id);
            var howTos = await this.howToProvider.GetAllByRecipeId(recipe.Id);
            
            recipe.Ingredients = ingredients?.ToList();
            recipe.HowTo = howTos?.Select(x => x.Step).ToArray();

            return Ok(recipe);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRecipeDto model)
        {
            if(model == null || string.IsNullOrEmpty(model.Url)){
                return BadRequest("No url");
            }

            var uri = new Uri(model.Url);
            var settings = this.siteSettingsProvider.GetSourceSiteSettings(uri.Host);
    
            if(settings == null){
                return BadRequest("No settings found for that site");
            }

            if(await this.recipeProvider.Exist(model.Url)){
                return BadRequest("Recipe already exist");
            }

            var html = this.scraper.Run(uri.OriginalString);
            var parsedRecipe = this.RecipeParser.Parse(html, settings);

            var recipe = new UpdateRecipeDto();
            recipe.Title = parsedRecipe.Title;
            recipe.Image = parsedRecipe.Image;
            recipe.Description = parsedRecipe.Description;
            recipe.SourceUrl = model.Url;

            if(parsedRecipe.Tags != null){
                recipe.Tags = string.Join(';', parsedRecipe.Tags);
            }
            
            var id = this.recipeProvider.Save(recipe);

            foreach(var ingredient in parsedRecipe.Ingredients){
                var ingredientDto = new UpdateIngredientDto{
                    RecipeId = id,
                    Amount = ingredient.Amount,
                    Unit = ingredient.Unit,
                    Name = ingredient.Name
                };

                this.ingredientProvider.Save(ingredientDto);
            }

            foreach(var step in parsedRecipe.HowTo){
                var stepDto = new HowToStepDto{RecipeId = id, Step = step };
                this.howToProvider.Save(stepDto);
            }
                            
            return Ok(recipe);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateRecipeDto recipe)
        {
            this.recipeProvider.Save(recipe);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.howToProvider.DeleteByRecipeId(id);
            this.ingredientProvider.DeleteByRecipeId(id);
            this.recipeProvider.Delete(id);
        }
    }
}
