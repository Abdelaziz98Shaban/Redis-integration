using System.Linq.Expressions;

namespace Domain.IRepositories;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>?> GetAll();
    public IQueryable<T> GetQuery();

    Task<T?> GetById(Expression<Func<T, bool>> expression);

    Task AddAsync(T entity);
    public void Update(T entity, params Expression<Func<T, object?>>?[]? properties);

    void Remove(T entity);
    public Task<bool> AnyAsync(Expression<Func<T, bool>> Predicate);

}
