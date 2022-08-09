using System.Linq.Expressions;
using Caffe.Application.Common.Interfaces.Base;
using Caffe.Domain.Common;
using Caffe.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Caffe.Infrastructure.Services.Base;

public class Repo<T> : IRepo<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> Entities;

    public Repo(ApplicationDbContext context)
    {
        Context = context;
        Entities = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await
            (from e in Entities
             select e).ToListAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> condition)
    {
        return await Entities.Where(condition).ToListAsync();
    }

    public List<T> GetAll()
    {
        return (from e in Entities select e).ToList();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return (await
            (from e in Entities
             where e.Id == id
             select e)
            .FirstOrDefaultAsync())!;
    }

    public T GetById(Guid id)
    {
        return (from e in Entities
                where e.Id == id
                select e).FirstOrDefault()!;
    }

    public async Task InsertAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        await Entities.AddAsync(entity);
    }

    public void Insert(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        Entities.Add(entity);
    }

    public async Task BulkInsertAsync(IEnumerable<T> entities)
    {
        if (entities is null)
            throw new ArgumentNullException(nameof(entities));

        await Entities.AddRangeAsync(entities);
    }

    public void BulkInsert(IEnumerable<T> entities)
    {
        if (entities is null)
            throw new ArgumentNullException(nameof(entities));

        Entities.AddRange(entities);
    }

    public void Delete(T entity)
    {
        Entities.Remove(entity);
    }

    public IQueryable<T> Query(params Expression<Func<T, object>>[] navigationProperties)
    {
        return navigationProperties.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(Entities,
            (current, navigationProperty) => current.Include(navigationProperty!));
    }

    public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> where,
        params Expression<Func<T, object>>[] navigationProperties)
    {
        var dbQuery = navigationProperties.Aggregate<Expression<Func<T, object>>, IQueryable<T>>
            (Entities, (current, navigationProperty) => current.Include(navigationProperty));
        return await dbQuery.Where(where).ToListAsync();
    }

}