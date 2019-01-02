using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;

namespace Feedbag.DataAccess.Repositories{
    public interface IRecipeRepository{
        Task<IEnumerable<Recipe>> GetAll();
        Task<Recipe> Get(int id);
        int Update(Recipe recipe);
        void Remove(Guid id);
    }
}