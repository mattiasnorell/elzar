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
                    return conn.QueryFirst<int>(@"INSERT INTO Recipes (Title, Image, Description, SourceUrl, Tags, CreatedAtUtc, UpdatedAtUtc) VALUES (@Title, @Image, @Description, @SourceUrl, @Tags, @CreatedAtUtc, @UpdatedAtUtc); select last_insert_rowid()", recipe);
                }else{
                    conn.Execute(@"UPDATE Recipes SET Title = @Title, Image = @Image, Description = @Description, SourceUrl = @SourceUrl, Tags = @Tags, UpdatedAtUtc = @UpdatedAtUtc WHERE Id = @id", recipe);

                    return recipe.Id;
                }
            }
        }

        private void CreateDatabase()
        {
            var fs = System.IO.File.Create("DbFile");
            fs.Close();

            using (var conn = this.dbConnection.GetOpenConnection())
            {
                conn.Open();
                conn.Execute(
                    @"CREATE TABLE `Projects` (
                    `Id`	INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    `ProjectName`	TEXT(100),
                    `ProjectId`	TEXT(100),
                    `Version`	TEXT(100),
                    `LastDeployAtUtc`	INTEGER)");

                conn.Execute(
                    @"CREATE TABLE `Logs` (
                    `Id`	INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    `ProjectId`	TEXT(100),
                    `ProjectVersion`	TEXT(100),
                    `LogText`	TEXT,
                    `CreatedAtUtc`	INTEGER)");

                conn.Execute(
                    @"CREATE TABLE `Environments` (
                    `Id`	INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    `EnvironmentName`	TEXT(100),
                    `Destination`	TEXT(255))");

                conn.Execute(
                    @"CREATE TABLE `BuildSteps` (
                    `Id`	INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    `EnvironmentId`	INTEGER,
                    `Application` TEXT(255),
					`Arguments` TEXT(255))");
            }
        }
    }
}