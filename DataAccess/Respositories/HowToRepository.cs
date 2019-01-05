using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;
using Dapper;
using Microsoft.Extensions.Options;
using Feedbag.Models;

namespace Feedbag.DataAccess.Repositories{

    public class HowToRepository : IHowToRepository
    {
        private readonly IOptions<ConnectionStrings> settings;

        private SQLiteConnection DatabaseConnection(){
           return new SQLiteConnection(this.settings.Value.FeedbagDatabase);
        }

        public HowToRepository(IOptions<ConnectionStrings> settings)
        {
            this.settings = settings;
        }

        public async Task<IEnumerable<HowToStep>> GetAllByRecipeId(int id)
        {
            using (var conn = DatabaseConnection())
            {
                return await conn.QueryAsync<HowToStep>(@"select * from HowToSteps where RecipeId = @id", new { id });
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

        public void Update(HowToStep step)
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