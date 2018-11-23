using System;
using System.Collections.Generic;
using Feedbag.Models;

namespace Feedbag.Business.Repositories{
    public interface IRecipeRepository{
        RecipeDto Get(Guid id);
        List<RecipeDto> GetAll();
        void Save(UpdateRecipeDto recipe);
        void Delete(Guid id);
    }
}