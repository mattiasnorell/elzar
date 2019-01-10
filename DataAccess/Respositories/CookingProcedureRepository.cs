using System.Collections.Generic;
using System.Threading.Tasks;
using Elzar.DataAccess.Entites;
using Dapper;
using Elzar.DataAccess.Providers;

namespace Elzar.DataAccess.Repositories
{

    public class CookingProcedureRepository : ICookingProcedureRepository
    {
        private readonly IDbConnectionProvider dbConnection;

        public CookingProcedureRepository(IDbConnectionProvider dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<CookingProcedureStep>> GetAllByRecipeId(int id)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                return await conn.QueryAsync<CookingProcedureStep>(@"select * from CookingProcedures where RecipeId = @id", new { id });
            }
        }

        public async void Delete(int id)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                await conn.ExecuteAsync(@"DELETE FROM CookingProcedures WHERE Id = @id", new { id });
            }
        }

        public async void DeleteByRecipeId(int id)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                await conn.ExecuteAsync(@"DELETE FROM CookingProcedures WHERE RecipeId = @id", new { id });
            }
        }

        public void Update(CookingProcedureStep step)
        {
            using (var conn = this.dbConnection.GetOpenConnection())
            {
                if(step.Id == 0){
                    conn.Execute(@"INSERT INTO CookingProcedures (RecipeId, Step) VALUES (@RecipeId, @Step)", step);
                }else{
                    conn.Execute(@"UPDATE CookingProcedures SET Step = @Step WHERE Id = @Id", step);
                }
            }
        }
    }
}