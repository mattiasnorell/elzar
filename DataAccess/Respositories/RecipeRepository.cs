using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;
using Dapper;
using Feedbag.Models;
using Microsoft.Extensions.Options;

namespace Feedbag.DataAccess.Repositories
{

    public class RecipeRepository : IRecipeRepository
    {
        private readonly Microsoft.Extensions.Options.IOptions<ConnectionStrings> settings;

        private SQLiteConnection DatabaseConnection(){
           /* if (!System.IO.File.Exists(this.settings.Value?.FeedbagDatabase))
            {
                throw new Exception("Database not found");
                //this.CreateDatabase();
            } */
            
            return new SQLiteConnection(this.settings.Value.FeedbagDatabase);
        }

        public RecipeRepository(IOptions<ConnectionStrings> settings)
        {
            this.settings = settings;
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            using (var conn = DatabaseConnection())
            {
                return await conn.QueryAsync<Recipe>(@"select * from Recipes");
            }
        }

        public async Task<Recipe> Get(int id)
        {
            using (var conn = DatabaseConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<Recipe>(@"select * from Recipes where Id = @id", new {id});
            }
        }

        public void Remove(Guid id)
        {
            
        }

        public int Update(Recipe recipe)
        {
            using (var conn = DatabaseConnection())
            {
                if(recipe.Id == 0){
                    return conn.QueryFirst<int>(@"INSERT INTO Recipes (Title, Image, Description, SourceUrl, CreatedAtUtc, UpdatedAtUtc) VALUES (@Title, @Image, @Description, @SourceUrl, @CreatedAtUtc, @UpdatedAtUtc); select last_insert_rowid()", recipe);
                }else{
                    conn.Execute(@"UPDATE Recipes SET Title = @Title, Image = @Image, Description = @Description, SourceUrl = @SourceUrl, UpdatedAtUtc = @UpdatedAtUtc WHERE Id = @id", recipe);

                    return recipe.Id;
                }
            }
        }

        private void CreateDatabase()
        {
            var fs = System.IO.File.Create("DbFile");
            fs.Close();

            using (var conn = DatabaseConnection())
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