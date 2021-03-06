using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elzar.DataAccess.Entites;

namespace Elzar.DataAccess.Repositories{
    public interface IRecipeRepository{
        Task<IEnumerable<Recipe>> GetAll();
        Task<Recipe> Get(int id);
        Task<IEnumerable<Recipe>> GetByTags(string[] tags);
        int Update(Recipe recipe);
        void Delete(int id);
    }
}