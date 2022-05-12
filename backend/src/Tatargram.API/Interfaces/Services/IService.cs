using System.Linq.Expressions;
using Tatargram.QueryModels;

namespace Tatargram.Interfaces.Services;

public interface IService<TEntity, TQueryModel>
{
    Task Create(TQueryModel model);
    Task Update(UpdateBaseQueryModel model);
    Task Delete(TEntity model);
    Task Delete(Guid id);
    Task<IEnumerable<TViewModel>> GetAll<TViewModel>(Expression<Func<TEntity, bool>> expression = null!, Expression<Func<TEntity, object>> include = null!, bool disableTracking = false);
    Task<TViewModel> GetById<TViewModel>(Guid id, Expression<Func<TEntity, object>> include = null!, bool disableTracking = false);
}