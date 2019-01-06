using Feedbag.Business.Providers;
using Feedbag.Models;
using Microsoft.AspNetCore.Mvc;

namespace Feedbag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HowToController : ControllerBase
    {
        private readonly IHowToProvider howToProvider;
        
        public HowToController(
            IHowToProvider howToProvider
        ){
            this.howToProvider = howToProvider;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] HowToStepDto howTo)
        {
            this.howToProvider.Save(howTo);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.howToProvider.Delete(id);
        }
    }
}
