using System.Data;

namespace Elzar.DataAccess.Providers{
    public interface IDbConnectionProvider{
        IDbConnection GetOpenConnection();
    }
}