using System.Linq.Expressions;
using MongoDB.Driver;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public BaseRepository()
        {
            var ctx = new MongoContext();
            _collection = ctx.GetMongoCollection<T>();
        }
        public async Task<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            var document = await _collection.Find(predicate).FirstOrDefaultAsync();
            return document;
        }

        public async Task<T> Save(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }
    }
}
