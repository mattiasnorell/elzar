using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;

namespace Feedbag.DataAccess.Repositories{
    public interface IHowToRepository{
        Task<IEnumerable<HowToStep>> GetAllByRecipeId(int id);
        void Update(HowToStep step);
        void Remove(int id);
    }
}