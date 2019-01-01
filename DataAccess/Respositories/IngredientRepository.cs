using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;
using Dapper;

namespace Feedbag.DataAccess.Repositories{

    public class IngredientRepository : IIngredientRepository
    {
        private static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\database\\feedbag.db"; }
        }

        private SQLiteConnection DatabaseConnection(){
            if (!System.IO.File.Exists(DbFile))
            {
                throw new Exception("No database found");
            }
            
            return new SQLiteConnection("Data Source=" + DbFile + ";DateTimeKind=Utc");
        }

        public IngredientRepository()
        {
    
        }

        public async Task<IEnumerable<Ingredient>> GetAllByRecipeId(int id)
        {
            using (var conn = DatabaseConnection())
            {
                return await conn.QueryAsync<Ingredient>(@"select * from Ingredients where RecipeId = @id", new { id });
            }
        }

        public async void Remove(int id)
        {
            using (var conn = DatabaseConnection())
            {
                await conn.ExecuteAsync(@"DELETE FROM Ingredients WHERE RecipeId = @id", new { id });
            }
        }

        public void Update(Ingredient ingredient)
        {
            using (var conn = DatabaseConnection())
            {
                if(ingredient.Id == 0){
                    conn.Execute(@"INSERT INTO Ingredients (RecipeId, Amount, Unit, Name) VALUES (@RecipeId, @Amount, @Unit, @Name)", ingredient);
                }else{
                    conn.Execute(@"UPDATE Ingredients SET Amout = @Amount, Unit = @Unit, Name = @Name WHERE Id = @Id", ingredient);
                }
            }
        }
    }
}