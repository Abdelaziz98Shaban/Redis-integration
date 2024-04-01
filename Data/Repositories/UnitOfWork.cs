using Data.Contexts;
using Domain.IRepositories;
using Domain.Models;
namespace Data.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
    }


    #region Item
    private IBaseRepository<Item> _ItemRepository;

    public IBaseRepository<Item> ItemRepository
    {

        get
        {
            if (_ItemRepository == null)
                _ItemRepository = new BaseRepository<Item>(_db);
            return _ItemRepository;
        }
    }
    #endregion

    #region Product
    private IBaseRepository<UOM> _UOMRepository;

    public IBaseRepository<UOM> UOMRepository
    {

        get
        {
            if (_UOMRepository == null)
                _UOMRepository = new BaseRepository<UOM>(_db);
            return _UOMRepository;
        }
    }
    #endregion


    public async Task<int> SaveAsync()
    {
     return await  _db.SaveChangesAsync();
    }


}

