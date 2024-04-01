using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Data.Services.Interfaces;

public interface IUOMService
{
    public Task<bool> AddAsync(UOMVM UOMVM);

    public Task<bool> UpdateAsync(UOMVM UOMVM);
    public Task<bool> DeleteAsync(int id);

    public Task<bool> AnyAsync(int id);

    public Task<UOMVM?> GetUOMVMAsync(int id);

    public Task<IEnumerable<UOMVM>?> GetAllUOMVMAsync();
    public Task<SelectList> GetSelectListAsync(int? selectedId = null);

}
