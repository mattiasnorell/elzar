using System;
using System.Collections.Generic;
using Feedbag.Models;

namespace Feedbag.Business.Providers{
    public interface IRecipeProvider{
        RecipeDto Get(Guid id);
        IEnumerable<RecipeDto> GetAll();
        void Save(UpdateRecipeDto recipe);
        void Delete(Guid id);
    }
}