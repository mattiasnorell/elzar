using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.Models;

namespace Feedbag.Business.Providers{
    public interface IRecipeProvider{
        Task<RecipeDto> Get(int id);
        Task<IEnumerable<RecipeDto>> GetAll();
        int Save(RecipeDto recipe);
        void Delete(Guid id);
        Task<Boolean> Exist(string url);
    }
}