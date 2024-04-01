namespace Domain.IRepositories;

public interface IUnitOfWork
{
    public Task<int> SaveAsync();
}
