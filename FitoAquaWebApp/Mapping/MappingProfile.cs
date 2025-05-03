using AutoMapper;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Obra, ObraDto>()
     .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.Name));

        CreateMap<ObraDto, Obra>()
            .ForMember(dest => dest.Cliente, opt => opt.Ignore());



        CreateMap<Material, MaterialDto>().ReverseMap();


    }
}
