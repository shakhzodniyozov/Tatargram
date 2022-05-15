using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tatargram.Interfaces.Repositories;
using Tatargram.Interfaces.Services;
using Tatargram.Models;
using Tatargram.QueryModels;

namespace Tatargram.Services;

public class BaseService<TEntity, TQueryModel> : IService<TEntity, TQueryModel>
{
    public BaseService(IRepository<TEntity> repository,
                        IMapper mapper,
                        UserManager<User> userManager,
                        IHttpContextAccessor contextAccessor)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.contextAccessor = contextAccessor;
        this.userManager = userManager;
    }

    protected readonly IHttpContextAccessor contextAccessor;
    protected readonly UserManager<User> userManager;
    protected readonly IRepository<TEntity> repository;
    protected readonly IMapper mapper;

    public virtual async Task Create(TQueryModel model)
    {
        await repository.Create(mapper.Map<TEntity>(model));
    }

    public async Task Delete(TEntity model)
    {
        await repository.Delete(model);
    }

    public virtual async Task Delete(Guid id)
    {
        await repository.Delete(id);
    }

    public async Task Delete(TQueryModel model)
    {
        await repository.Delete(mapper.Map<TEntity>(model));
    }

    public virtual async Task<IEnumerable<TViewModel>> GetAll<TViewModel>(Expression<Func<TEntity, bool>> expression = null!,
                                                                        Expression<Func<TEntity, object>> include = null!,
                                                                        bool disableTracking = false)
    {
        var entities = await repository.GetAll(expression, include, disableTracking);

        return mapper.Map<IEnumerable<TViewModel>>(entities);
    }

    public virtual async Task<TViewModel> GetById<TViewModel>(Guid id, Expression<Func<TEntity, object>> include = null!, bool disableTracking = false)
    {
        var entity = await repository.GetById(id, include, disableTracking);

        return mapper.Map<TViewModel>(entity);
    }

    public virtual async Task Update(UpdateBaseQueryModel model)
    {
        var entity = await repository.GetById(model.Id);

        if (entity == null)
            throw new NotFoundException("Entity with provided id was not found");

        mapper.Map(model, entity);
        await repository.Update(entity);
    }
}