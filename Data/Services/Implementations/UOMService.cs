using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Data.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Data.Services.Implementations
{
    public class UOMService : IUOMService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _UoW;
        public UOMService(IMapper mapper, UnitOfWork uoW)
        {
            _mapper = mapper;
            _UoW = uoW;
        }



        public async Task<bool> AddAsync(UOMVM UOMVM)
        {
            try
            {
                var UOM = _mapper.Map<Domain.Models.UOM>(UOMVM);

                await _UoW.UOMRepository.AddAsync(UOM);
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

        public async Task<bool> UpdateAsync(UOMVM UOMVM)
        {

            try
            {
                var UOM = _mapper.Map<Domain.Models.UOM>(UOMVM);
                _UoW.UOMRepository.Update(UOM);
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
                var city = await _UoW.UOMRepository.GetById(e => e.Id == id);
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
            return await _UoW.UOMRepository.AnyAsync(e => e.Id == id);
        }

        public async Task<UOMVM?> GetUOMVMAsync(int id)
        {

            var query = _UoW.UOMRepository.GetQuery();

            return await query.Where(e => e.Id == id).AsNoTracking()
                                .ProjectTo<UOMVM>(_mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<UOMVM>?> GetAllUOMVMAsync()
        {
            var query = _UoW.UOMRepository.GetQuery();

            return await query.AsNoTracking()
                                .ProjectTo<UOMVM>(_mapper.ConfigurationProvider)
                                .ToListAsync();
        }


        public async Task<SelectList> GetSelectListAsync(int? selectedId = null)
        {
            var query = _UoW.UOMRepository.GetQuery();

            var uoms = await query.AsNoTracking()
                                 .Select(e => new { Id = e.Id, Name = e.Name })
                                .ToListAsync();

            return new SelectList(uoms, "Id", "Name", selectedId);

        }
    }
}
