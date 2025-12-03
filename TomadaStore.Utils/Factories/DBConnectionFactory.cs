using System.Data;
using TomadaStore.Utils.Interfaces;

namespace TomadaStore.Utils.Factories
{
    public abstract class DBConnectionFactory
    {
        public abstract IDBConnection CreateConnection();

        public string GetDBConnection()
        {
            var dbConnection = CreateConnection();
            return dbConnection.ConnectionString();
        }
    }
}
