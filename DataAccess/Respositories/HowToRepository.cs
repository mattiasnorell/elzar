using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Feedbag.DataAccess.Entites;
using Dapper;

namespace Feedbag.DataAccess.Repositories{

    public class HowToRepository : IHowToRepository
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

        public HowToRepository()
        {
    
        }

        public async Task<IEnumerable<HowToStep>> GetAllByRecipeId(int id)
        {
            using (var conn = DatabaseConnection())
            {
                return await conn.QueryAsync<HowToStep>(@"select * from HowToSteps where RecipeId = @id", new { id });
            }
        }

        public async void Remove(int id)
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