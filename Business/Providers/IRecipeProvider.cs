using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.Models;

namespace Feedbag.Business.Providers{
    public interface IRecipeProvider{
        Task<RecipeDto> GetAsync(Guid id);
        Task<IEnumerable<RecipeDto>> GetAllAsync();
        void Save(RecipeDto recipe);
        void Delete(Guid id);
    }
}