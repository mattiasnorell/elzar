using System;
using System.Collections.Generic;
using Feedbag.DataAccess.Entites;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Feedbag.DataAccess.Repositories{

    public class RecipeRepository : IRecipeRepository
    {
        private readonly MongoDB.Driver.IMongoDatabase database;

        public RecipeRepository()
        {
            var client = new MongoClient("");
            this.database = client.GetDatabase("feedbag");
        }

        public IMongoCollection<Recipe> GetAll()
        {
            var collection = this.database.GetCollection<Recipe>("recepies");

            return collection;
        }

        public Recipe Get(Guid id)
        {
            var collection = this.database.GetCollection<Recipe>("recepies");
            return collection.Find<Recipe>(new BsonDocument("id", id)).Single();
        }

        public void Remove(Guid id)
        {
            var collection = this.database.GetCollection<Recipe>("recepies");
            collection.DeleteOne(new BsonDocument("id", id));
        }

        public void Update(Recipe recipe)
        {
            var collection = this.database.GetCollection<Recipe>("recepies");
            //collection.FindOneAndUpdate<Recipe>(recipe);
        }
    }
}