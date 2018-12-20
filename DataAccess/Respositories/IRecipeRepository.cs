using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;

namespace Feedbag.DataAccess.Repositories{
    public interface IRecipeRepository{
        Task<IEnumerable<Recipe>> GetAll();
        Task<Recipe> Get(Guid id);
        void Update(Recipe recipe);
        void Remove(Guid id);
    }
}