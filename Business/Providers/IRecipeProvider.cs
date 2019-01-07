using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elzar.Models;

namespace Elzar.Business.Providers{
    public interface IRecipeProvider{
        Task<RecipeDto> Get(int id);
        Task<IEnumerable<RecipeDto>> GetAll();
        Task<IEnumerable<RecipeDto>> GetByTags(string[] tags);
        int Save(UpdateRecipeDto recipe);
        void Delete(int id);
        Task<Boolean> Exist(string url);
    }
}