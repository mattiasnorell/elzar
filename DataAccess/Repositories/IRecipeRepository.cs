using System;
using System.Collections.Generic;
using Feedbag.DataAccess.Entites;

namespace Feedbag.DataAccess.Repositories{
    public interface IRecipeRepository{
        List<Recipe> GetAll();
        Recipe Get(Guid id);
    }
}