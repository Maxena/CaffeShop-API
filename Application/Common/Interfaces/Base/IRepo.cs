using System.Linq.Expressions;
using Caffe.Domain.Common;

namespace Caffe.Application.Common.Interfaces.Base;

public interface IRepo<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> condition);
    List<T> GetAll();

    Task<T> GetByIdAsync(Guid id);
    T GetById(Guid id);

    Task InsertAsync(T entity);
    void Insert(T entity);

    Task BulkInsertAsync(IEnumerable<T> entities);
    void BulkInsert(IEnumerable<T> entities);
    void Delete(T entity);

    IQueryable<T> Query(params Expression<Func<T, object>>[] navigationProperties);

    Task<List<T>> QueryAsync(Expression<Func<T, bool>> where,
        params Expression<Func<T, object>>[] navigationProperties);
}