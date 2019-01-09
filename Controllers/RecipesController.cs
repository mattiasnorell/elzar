using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elzar.Business.Parser;
using Elzar.DataAccess.Providers;
using Elzar.Business.Scraper;
using Elzar.Business.Providers;
using Elzar.Models;
using Elzar.Pdf;
using Microsoft.AspNetCore.Mvc;
using Elzar.Business.Mappers;
using Elzar.FileHandlers;

namespace Elzar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly ICookingProcedureProvider cookingProcedureProvider;
        private readonly IIngredientProvider ingredientProvider;
        private readonly IIngredientMapper ingredientMapper;
        private readonly IRecipeProvider recipeProvider;
        private readonly IRecipeMapper recipeMapper;
        private readonly IPdfGenerator pdfGenerator;
        private readonly IScraper scraper;
        private readonly IRecipeParser RecipeParser;
        private readonly ISiteSettingsProvider siteSettingsProvider;
        private readonly IFileHandler fileHandler;

        public RecipesController(
            ICookingProcedureProvider cookingProcedureProvider,
            IIngredientProvider ingredientProvider,
            IIngredientMapper ingredientMapper, 
            IRecipeProvider recipeProvider, 
            IRecipeMapper recipeMapper, 
            IPdfGenerator pdfGenerator,
            IScraper scraper, 
            IRecipeParser RecipeParser, 
            ISiteSettingsProvider siteSettingsProvider,
            IFileHandler fileHandler
        ){
            this.cookingProcedureProvider = cookingProcedureProvider;
            this.ingredientProvider = ingredientProvider;
            this.ingredientMapper = ingredientMapper;
            this.recipeProvider = recipeProvider;
            this.recipeMapper = recipeMapper;
            this.pdfGenerator = pdfGenerator;
            this.scraper = scraper;
            this.RecipeParser = RecipeParser;
            this.siteSettingsProvider = siteSettingsProvider;
            this.fileHandler = fileHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetAsync()
        {
            var recipes = await this.recipeProvider.GetAll();
            var returnValue = new List<RecipeDto>();

            foreach(var recipe in recipes){
                var ingredients = await this.ingredientProvider.GetAllByRecipeId(recipe.Id);
                var howTos = await this.cookingProcedureProvider.GetAllByRecipeId(recipe.Id);
                
                var mappedRecipe = new RecipeDto();

                mappedRecipe.Id = recipe.Id;
                mappedRecipe.Title = recipe.Title;
                mappedRecipe.Description = recipe.Description;
                mappedRecipe.Image = recipe.Image;
                mappedRecipe.SourceUrl = recipe.SourceUrl;
                mappedRecipe.Ingredients = ingredients?.ToList();
                mappedRecipe.CookingProcedureSteps = howTos?.Select(x => x.Step).ToArray();
                
                returnValue.Add(mappedRecipe);
            }
            
            return Ok(returnValue);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetAsync(int id)
        {
            var recipe = await this.recipeProvider.Get(id);

            if(recipe == null){
                return NotFound();
            }

            var ingredients = await this.ingredientProvider.GetAllByRecipeId(recipe.Id);
            var howTos = await this.cookingProcedureProvider.GetAllByRecipeId(recipe.Id);
            
            recipe.Ingredients = ingredients?.ToList();
            recipe.CookingProcedureSteps = howTos?.Select(x => x.Step).ToArray();

            return Ok(recipe);
        }

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

            foreach(var step in parsedRecipe.CookingProcedureSteps){
                var stepDto = new CookingProcedureDto{RecipeId = id, Step = step };
                this.cookingProcedureProvider.Save(stepDto);
            }

            this.fileHandler.Download(recipe.Image, id.ToString() + ".jpg");
            return Ok(id);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateRecipeDto recipe)
        {
            this.recipeProvider.Save(recipe);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.cookingProcedureProvider.DeleteByRecipeId(id);
            this.ingredientProvider.DeleteByRecipeId(id);
            this.fileHandler.Delete(id);
            this.recipeProvider.Delete(id);
        }
    }
}
