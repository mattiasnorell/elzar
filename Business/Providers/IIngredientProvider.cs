using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.Models;

namespace Feedbag.Business.Providers{
    public interface IIngredientProvider{
        Task<IEnumerable<IngredientDto>> GetAllByRecipeId(int id);
        void Save(IngredientDto ingredient);
        void Delete(int id);
    }
}