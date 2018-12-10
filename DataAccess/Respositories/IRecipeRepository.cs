using System;
using System.Collections.Generic;
using Feedbag.DataAccess.Entites;
using MongoDB.Driver;

namespace Feedbag.DataAccess.Repositories{
    public interface IRecipeRepository{
        IMongoCollection<Recipe> GetAll();
        Recipe Get(Guid id);
        void Update(Recipe recipe);
        void Remove(Guid id);
    }
}