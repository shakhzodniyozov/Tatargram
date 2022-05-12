using System.Linq.Expressions;

namespace Tatargram.Interfaces.Repositories;

public interface IRepository<T>
{
    Task Create(T model);
    Task Update(T model);
    Task Delete(T model);
    Task Delete(Guid id);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null!, Expression<Func<T, object>> include = null!, bool disableTracking = false);
    Task<T?> GetById(Guid id, Expression<Func<T, object>> include = null!, bool disableTracking = false);
}