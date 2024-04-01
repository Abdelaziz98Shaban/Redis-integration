using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Data.Services.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Services.Implementations
{
    public class ItemService :IItemService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _UoW;
        public ItemService(IMapper mapper, UnitOfWork uoW)
        {
            _mapper = mapper;
            _UoW = uoW;
        }


        public async Task<bool> AddAsync(ItemVM ItemVM)
        {
            try
            {
                var Item = _mapper.Map<Item>(ItemVM);

                await _UoW.ItemRepository.AddAsync(Item);
                if (await _UoW.SaveAsync() > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

            }

            return false;

        }

        public async Task<bool> UpdateAsync(ItemVM ItemVM)
        {

            try
            {
                var Item = _mapper.Map<Item>(ItemVM);
                _UoW.ItemRepository.Update(Item);
                if (await _UoW.SaveAsync() > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

            }

            return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var city = await _UoW.ItemRepository.GetById(e=>e.Id == id);
                if (city != null)
                {

                    city.Deleted = true;
                    if (await _UoW.SaveAsync() > 0)
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _UoW.ItemRepository.AnyAsync(e => e.Id == id );
        }

        public async Task<ItemVM?> GetItemVMAsync(int id)
        {

            var query = _UoW.ItemRepository.GetQuery();

            return await query.Where(e => e.Id == id).AsNoTracking()
                                .ProjectTo<ItemVM>(_mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<ItemVM>?> GetAllItemVMAsync()
        {
            var query = _UoW.ItemRepository.GetQuery();

            return await query.AsNoTracking()
                                .ProjectTo<ItemVM>(_mapper.ConfigurationProvider)
                                .ToListAsync();
        }
    }
}
