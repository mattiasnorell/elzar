using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elzar.DataAccess.Entites;

namespace Elzar.DataAccess.Repositories{
    public interface IIngredientRepository{
        Task<IEnumerable<Ingredient>> GetAllByRecipeId(int id);
        void Update(Ingredient ingredient);
        void Delete(int id);
        void DeleteByRecipeId(int id);
    }
}