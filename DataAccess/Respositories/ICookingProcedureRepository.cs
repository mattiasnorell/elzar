using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;

namespace Feedbag.DataAccess.Repositories{
    public interface ICookingProcedureRepository{
        Task<IEnumerable<CookingProcedureStep>> GetAllByRecipeId(int id);
        void Update(CookingProcedureStep step);
        void Delete(int id);
        void DeleteByRecipeId(int id);
    }
}