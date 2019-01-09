using System;
using System.Linq;
using System.Threading.Tasks;
using Elzar.Business.Providers;
using Elzar.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elzar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IRecipeProvider recipeProvider;
     
        public SearchController(
            IRecipeProvider recipeProvider
        ){
            this.recipeProvider = recipeProvider;
        }

        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TagSearchDto model)
        {
            var result = await this.recipeProvider.GetByTags(model.Tags);
            
            return Ok(result);
        }
    }
}
