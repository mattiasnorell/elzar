using System.Collections.Generic;
using System.Threading.Tasks;
using Elzar.DataAccess.Entites;
using Dapper;
using Elzar.DataAccess.Providers;

namespace Elzar.DataAccess.Repositories
{

    public class RecipeRepository : IRecipeRepository
    {
        private readonly IDbConnectionProvider dbConnection;

        public RecipeRepository(IDbConnectionProvider dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                return await conn.QueryAsync<Recipe>(@"select * from Recipes");
            }
        }

        public async Task<Recipe> Get(int id)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<Recipe>(@"select * from Recipes where Id = @id", new {id});
            }
        }

        public async Task<IEnumerable<Recipe>> GetByTags(string[] tags)
        {
            var likeStatement = new List<string>();

            foreach(var tag in tags){
                likeStatement.Add($"Tags LIKE '%{ tag }%'");
            }

            var sqlStatement = "SELECT * FROM Recipes WHERE " + string.Join(" OR ", likeStatement);

            using (var conn = this.dbConnection.GetOpenConnection())
            {
                return await conn.QueryAsync<Recipe>(sqlStatement);
            }
        }

        public void Delete(int id)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                conn.Execute(@"DELETE from Recipes where Id = @id", new {id});
            }
        }

        public int Update(Recipe recipe)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                if(recipe.Id == 0){
                    return conn.QueryFirst<int>(@"INSERT INTO Recipes (Title, Image, Description, SourceUrl, Tags, CreatedAtUtc, UpdatedAtUtc) VALUES (@Title, @Image, @Description, @SourceUrl, @Tags, @CreatedAtUtc, @UpdatedAtUtc); select LAST_INSERT_ID()", recipe);
                }else{
                    conn.Execute(@"UPDATE Recipes SET Title = @Title, Image = @Image, Description = @Description, SourceUrl = @SourceUrl, Tags = @Tags, UpdatedAtUtc = @UpdatedAtUtc WHERE Id = @id", recipe);

                    return recipe.Id;
                }
            }
        }
    }
}