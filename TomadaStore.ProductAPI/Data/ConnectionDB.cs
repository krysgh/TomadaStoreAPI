using Microsoft.Extensions.Options;
using MongoDB.Driver;

using TomadaStore.Models.Models;

namespace TomadaStore.ProductAPI.Data
{
    namespace TomadaStore.CustomerAPI.Data
    {
        public class ConnectionDB
        {
            public readonly IMongoCollection<Product> mongoCollection;
            public ConnectionDB(IOptions<MongoDBSettings> mongoDbSettings)
            {
                MongoClient client = new(mongoDbSettings.Value.ConnectionURI);
                IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DataBaseName);
                mongoCollection = database.GetCollection<Product>(mongoDbSettings.Value.CollectionName);
            }

            public IMongoCollection<Product> GetMongoCollection()
            {
                return mongoCollection;
            }
        }
    }
}
