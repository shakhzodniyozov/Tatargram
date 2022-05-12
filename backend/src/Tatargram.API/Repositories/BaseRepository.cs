using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tatargram.Data;
using Tatargram.Interfaces;
using Tatargram.Interfaces.Repositories;

namespace Tatargram.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly ApplicationDbContext context;
    protected readonly DbSet<T> entities;

    public BaseRepository(ApplicationDbContext context)
    {
        this.context = context;
        this.entities = context.Set<T>();
    }
    public async Task Create(T model)
    {
        await entities.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task Delete(T model)
    {
        entities.Remove(model);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);
        if (entity == null)
            throw new NotFoundException("Entity with provided id was not found");

        entities.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null!, Expression<Func<T, object>> include = null!, bool disableTracking = false)
    {
        IQueryable<T> query = entities;

        if (disableTracking)
            query = query.AsNoTracking();
        if (expression != null)
        {
            query = entities.Where(expression);
        }
        if (include != null)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    public async Task<T?> GetById(Guid id, Expression<Func<T, object>> include = null!, bool disableTracking = false)
    {
        IQueryable<T> query = entities;

        if (disableTracking)
            query = query.AsNoTracking();
        if (include != null)
            query = query.Include(include);

        return await entities.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(T model)
    {
        entities.Update(model);
        await context.SaveChangesAsync();
    }
}