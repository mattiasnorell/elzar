using Feedbag.Business.Providers;
using Feedbag.Models;
using Microsoft.AspNetCore.Mvc;

namespace Feedbag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientProvider ingredientProvider;
        
        public IngredientController(
            IIngredientProvider ingredientProvider
        ){
            this.ingredientProvider = ingredientProvider;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateIngredientDto ingredientDto)
        {
            this.ingredientProvider.Save(ingredientDto);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.ingredientProvider.Delete(id);
        }
    }
}
