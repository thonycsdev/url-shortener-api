using MongoDB.Driver;

namespace Repository.Interfaces
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetMongoCollection<T>();
    }
}
