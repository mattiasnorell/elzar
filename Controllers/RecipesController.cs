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
        private readonly IRecipeProvider recipeProvider;
        private readonly IRecipeMapper recipeMapper;
        private readonly IPdfGenerator pdfGenerator;
        private readonly IScraper scraper;
        private readonly IRecipeParser RecipeParser;
        private readonly ISiteSettingsProvider siteSettingsProvider;

        public RecipesController(IRecipeProvider recipeProvider, IRecipeMapper recipeMapper, IPdfGenerator pdfGenerator, IScraper scraper, IRecipeParser RecipeParser, ISiteSettingsProvider siteSettingsProvider){
            this.recipeProvider = recipeProvider;
            this.recipeMapper = recipeMapper;
            this.pdfGenerator = pdfGenerator;
            this.scraper = scraper;
            this.RecipeParser = RecipeParser;
            this.siteSettingsProvider = siteSettingsProvider;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<RecipeDto>> Get()
        {
            var recipes = this.recipeProvider.GetAllAsync();

            return Ok(recipes);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<RecipeDto> Get(Guid id)
        {
            var recipe = this.recipeProvider.GetAsync(id);

            return Ok(recipe);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] CreateRecipeDto model)
        {
            if(model == null || string.IsNullOrEmpty(model.Url)){
                return BadRequest("No url");
            }

            var uri = new Uri(model.Url);
            var settings = this.siteSettingsProvider.GetSourceSiteSettings(uri.Host);
    
            if(settings == null){
                return BadRequest("No settings found for that site");
            }

            var html = this.scraper.Run(uri.OriginalString);
            var parsedRecipe = this.RecipeParser.Parse(html, settings);
            var recipe = this.recipeMapper.ToDto(parsedRecipe);
            recipe.SourceUrl = model.Url;
                
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
        public void Delete(Guid id)
        {
            this.recipeProvider.Delete(id);
        }
    }
}
