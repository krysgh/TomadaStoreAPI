using Microsoft.Extensions.Configuration;
using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils.Factories
{
    internal class SqlDBConnectionImpl : IDBConnection
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public SqlDBConnectionImpl()
        {
            _connectionString = _configuration.GetConnectionString("SqlServer");
        }

        public string ConnectionString()
        {
            return _connectionString;
        }
    }
}
