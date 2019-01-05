using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.Models;

namespace Feedbag.Business.Providers{
    public interface IHowToProvider{
        Task<IEnumerable<HowToStepDto>> GetAllByRecipeId(int id);
        void Save(HowToStepDto recipe);
        void Delete(int id);
        void DeleteByRecipeId(int id);
    }
}