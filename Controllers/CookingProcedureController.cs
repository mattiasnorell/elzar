﻿using Elzar.Business.Providers;
using Elzar.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elzar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookingProcedureController : ControllerBase
    {
        private readonly ICookingProcedureProvider cookingProcedureProvider;
        
        public CookingProcedureController(
            ICookingProcedureProvider cookingProcedureProvider
        ){
            this.cookingProcedureProvider = cookingProcedureProvider;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CookingProcedureDto howTo)
        {
            this.cookingProcedureProvider.Save(howTo);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.cookingProcedureProvider.Delete(id);
        }
    }
}
