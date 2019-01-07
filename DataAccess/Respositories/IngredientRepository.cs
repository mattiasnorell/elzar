using System.Collections.Generic;
using System.Threading.Tasks;
using Elzar.DataAccess.Entites;
using Dapper;
using Elzar.DataAccess.Providers;

namespace Elzar.DataAccess.Repositories
{

    public class IngredientRepository : IIngredientRepository
    {
        private readonly IDbConnectionProvider dbConnection;

        public IngredientRepository(IDbConnectionProvider dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Ingredient>> GetAllByRecipeId(int id)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                return await conn.QueryAsync<Ingredient>(@"select * from Ingredients where RecipeId = @id", new { id });
            }
        }

        public async void Delete(int id)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                await conn.ExecuteAsync(@"DELETE FROM Ingredients WHERE Id = @id", new { id });
            }
        }

        public async void DeleteByRecipeId(int id)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                await conn.ExecuteAsync(@"DELETE FROM Ingredients WHERE RecipeId = @id", new { id });
            }
        }

        public void Update(Ingredient ingredient)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
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