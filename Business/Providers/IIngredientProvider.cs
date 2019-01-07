using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elzar.Models;

namespace Elzar.Business.Providers{
    public interface IIngredientProvider{
        Task<IEnumerable<IngredientDto>> GetAllByRecipeId(int id);
        void Save(UpdateIngredientDto ingredient);
        void Delete(int id);
        void DeleteByRecipeId(int id);
    }
}