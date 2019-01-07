using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.Models;

namespace Feedbag.Business.Providers{
    public interface ICookingProcedureProvider{
        Task<IEnumerable<CookingProcedureDto>> GetAllByRecipeId(int id);
        void Save(CookingProcedureDto recipe);
        void Delete(int id);
        void DeleteByRecipeId(int id);
    }
}