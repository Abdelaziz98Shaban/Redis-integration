using Domain.ViewModels;

namespace Data.Services.Interfaces;

public interface IItemService
{
    public Task<bool> AddAsync(ItemVM ItemVM);

    public Task<bool> UpdateAsync(ItemVM ItemVM);
    public Task<bool> DeleteAsync(int id);

    public Task<bool> AnyAsync(int id);

    public Task<ItemVM?> GetItemVMAsync(int id);

    public Task<IEnumerable<ItemVM>?> GetAllItemVMAsync();
}
