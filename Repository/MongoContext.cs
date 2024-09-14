using MongoDB.Driver;
using Repository.Interfaces;

namespace Repository
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongodb;
        public MongoContext()
        {
            _mongoClient = GetMongoClient();
            _mongodb = GetDatabaseInstance();
        }

        private IMongoClient GetMongoClient()
        {
            var connectionUri = Environment.GetEnvironmentVariable("MONGODB_CONNECTION");
            return new MongoClient(connectionUri);
        }

        private IMongoDatabase GetDatabaseInstance()
        {
            var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
            return _mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetMongoCollection<T>()
        {
            var collectionName = Environment.GetEnvironmentVariable("COLLECTION_NAME");
            return _mongodb.GetCollection<T>(collectionName);
        }

    }
}
