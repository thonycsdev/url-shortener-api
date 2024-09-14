using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetBy(Expression<Func<T, bool>> predicate);
        Task<T> Save(T entity);
    }
}
