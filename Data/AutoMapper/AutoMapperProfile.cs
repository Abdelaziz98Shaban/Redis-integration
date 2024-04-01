using AutoMapper;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Data.AutoMapper;
public class AutoMapperProfile : Profile
{

    public AutoMapperProfile()
    {
        CreateMap<IdentityResult, IdentityResultViewModel>()
         .ReverseMap();

        CreateMap<IdentityError, ErrorRequestViewModel>().ReverseMap();
        CreateMap<UOM, UOMVM>().ReverseMap();
        CreateMap<Item, ItemVM>()
            .ReverseMap()
            .ForMember(e => e.UOM, s => s.Ignore());

    }

}

