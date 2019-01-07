using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elzar.Models;

namespace Elzar.Business.Providers{
    public interface ICookingProcedureProvider{
        Task<IEnumerable<CookingProcedureDto>> GetAllByRecipeId(int id);
        void Save(CookingProcedureDto recipe);
        void Delete(int id);
        void DeleteByRecipeId(int id);
    }
}