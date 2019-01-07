using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elzar.DataAccess.Entites;

namespace Elzar.DataAccess.Repositories{
    public interface ICookingProcedureRepository{
        Task<IEnumerable<CookingProcedureStep>> GetAllByRecipeId(int id);
        void Update(CookingProcedureStep step);
        void Delete(int id);
        void DeleteByRecipeId(int id);
    }
}