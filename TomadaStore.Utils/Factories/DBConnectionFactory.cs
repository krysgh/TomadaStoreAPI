using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils.Factories
{
    public abstract class DbConnectionFactory
    {
        public abstract IDBConnection CreateDBConnection();

        public string GetConnectionString()
        {
            var dbConnection = CreateDBConnection();
            return dbConnection.ConnectionString();
        }
    }
}
