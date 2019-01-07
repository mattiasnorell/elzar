using System;
using System.Data;
using System.Data.SQLite;
using Elzar.Models;
using Microsoft.Extensions.Options;

namespace Elzar.DataAccess.Providers{
    public class SQLiteConnectionProvider: IDbConnectionProvider{
        private readonly IOptions<ConnectionStrings> settings;

        public SQLiteConnectionProvider(IOptions<ConnectionStrings> settings){
            this.settings = settings;
        }

        public IDbConnection GetOpenConnection(){
            if(string.IsNullOrEmpty(this.settings.Value?.Database)){
                throw new Exception("No database connectionstring found");
            }

           return new SQLiteConnection(this.settings.Value.Database);
        }
    }
}