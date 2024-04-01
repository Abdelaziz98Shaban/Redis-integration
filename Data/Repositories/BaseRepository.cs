using Data.Contexts;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{

    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public BaseRepository(ApplicationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }
    public async Task AddAsync(T entity)
    {
      await  dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await dbSet.ToListAsync();

    }
    public IQueryable<T> GetQuery()
    {
        return dbSet;
    }

    public virtual void Update(T entity, params Expression<Func<T, object?>>?[]? properties)
    {
        if (_db.Entry(entity).State == EntityState.Detached)
        {
            _db.Attach(entity);

        }
        _db.Entry(entity).State = EntityState.Modified;

        if (properties != null)
        {
            foreach (var property in properties)
            {
                if (property != null && property.Body != null)
                {
                    var propertyName = Helpers.Helper.GetPropertyName(property.Body);

                    if (propertyName != null)
                    {
                        _db.Entry(entity).Property(propertyName).IsModified = false;
                    }
                }
            }
        }
    }
    public async Task<T?> GetById(Expression<Func<T, bool>> expression)
    {
        return await dbSet.FirstOrDefaultAsync(expression);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> Predicate)
    {
        return await dbSet.AnyAsync(Predicate);
    }


}
