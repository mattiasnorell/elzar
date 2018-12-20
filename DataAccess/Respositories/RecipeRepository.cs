using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Feedbag.DataAccess.Repositories{

    public class RecipeRepository : IRecipeRepository
    {
        private IMongoDatabase database;

        public RecipeRepository()
        {
            var client = new MongoClient("");
            this.database = client.GetDatabase("feedbag");
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            var collection = this.database.GetCollection<Recipe>("recepies");

            return await collection.Find<Recipe>(_ => true).ToListAsync();
        }

        public async Task<Recipe> Get(Guid id)
        {
            var collection = this.database.GetCollection<Recipe>("recepies");
            return await collection.Find<Recipe>(x => x.Id == id).SingleAsync();
        }

        public void Remove(Guid id)
        {
            var collection = this.database.GetCollection<Recipe>("recepies");
            collection.DeleteOneAsync(x => x.Id == id);
        }

        public void Update(Recipe recipe)
        {
            var collection = this.database.GetCollection<Recipe>("recepies");
            //collection.UpdateOneAsync<Recipe>(recipe);
        }
    }
}