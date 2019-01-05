using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;

namespace Feedbag.DataAccess.Repositories{
    public interface IIngredientRepository{
        Task<IEnumerable<Ingredient>> GetAllByRecipeId(int id);
        void Update(Ingredient ingredient);
        void Delete(int id);
        void DeleteByRecipeId(int id);
    }
}