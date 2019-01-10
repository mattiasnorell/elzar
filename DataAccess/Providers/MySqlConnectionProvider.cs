using System;
using System.Data;
using MySql.Data;
using Elzar.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Elzar.DataAccess.Providers{
    public class MySqlConnectionProvider: IDbConnectionProvider{
        private readonly IOptions<ConnectionStrings> settings;

        public MySqlConnectionProvider(IOptions<ConnectionStrings> settings){
            this.settings = settings;
        }

        public IDbConnection GetOpenConnection(){
            if(string.IsNullOrEmpty(this.settings.Value?.Database)){
                throw new Exception("No database connectionstring found");
            }

           return new MySqlConnection(this.settings.Value.Database);
        }
    }
}