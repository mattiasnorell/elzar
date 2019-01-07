using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Elzar.DataAccess.Entites;
using Dapper;
using Microsoft.Extensions.Options;
using Elzar.Models;

namespace Elzar.DataAccess.Repositories
{

    public class CookingProcedureRepository : ICookingProcedureRepository
    {
        private readonly IOptions<ConnectionStrings> settings;

        private SQLiteConnection DatabaseConnection(){
           return new SQLiteConnection(this.settings.Value.Database);
        }

        public CookingProcedureRepository(IOptions<ConnectionStrings> settings)
        {
            this.settings = settings;
        }

        public async Task<IEnumerable<CookingProcedureStep>> GetAllByRecipeId(int id)
        {
            using (var conn = DatabaseConnection())
            {
                return await conn.QueryAsync<CookingProcedureStep>(@"select * from HowToSteps where RecipeId = @id", new { id });
            }
        }

        public async void Delete(int id)
        {
            using (var conn = DatabaseConnection())
            {
                await conn.ExecuteAsync(@"DELETE FROM HowToSteps WHERE Id = @id", new { id });
            }
        }

        public async void DeleteByRecipeId(int id)
        {
            using (var conn = DatabaseConnection())
            {
                await conn.ExecuteAsync(@"DELETE FROM HowToSteps WHERE RecipeId = @id", new { id });
            }
        }

        public void Update(CookingProcedureStep step)
        {
            using (var conn = DatabaseConnection())
            {
                if(step.Id == 0){
                    conn.Execute(@"INSERT INTO HowToSteps (RecipeId, Step) VALUES (@RecipeId, @Step)", step);
                }else{
                    conn.Execute(@"UPDATE HowToSteps SET Step = @Step WHERE Id = @Id", step);
                }
            }
        }
    }
}