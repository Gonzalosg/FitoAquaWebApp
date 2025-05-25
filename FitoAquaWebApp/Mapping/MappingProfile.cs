using AutoMapper;
using FitoAquaWebApp.Dto;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeo de Obra
        CreateMap<Obra, ObraDto>()
            .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Name : ""))
            .ReverseMap();

        // Mapeo de Material
        CreateMap<Material, MaterialDto>().ReverseMap();

        // Mapeo de Usuario
        CreateMap<Usuario, UsuarioDto>().ReverseMap();

        // Mapeo de Albaran
        CreateMap<Albaran, AlbaranDto>()
            .ForMember(dest => dest.NombreObra, opt => opt.MapFrom(src => src.Obra.Nombre))
            .ForMember(dest => dest.NombreEmpleado, opt => opt.MapFrom(src => src.Empleado.Name))
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles))
            .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
            .ReverseMap()
            .ForMember(dest => dest.Obra, opt => opt.Ignore())
            .ForMember(dest => dest.Empleado, opt => opt.Ignore());

        // Mapeo de DetalleAlbaran
        CreateMap<DetalleAlbaran, AlbaranDetalleDto>()
            .ForMember(dest => dest.NombreMaterial, opt => opt.MapFrom(src => src.Material.Nombre))
            .ReverseMap()
            .ForMember(dest => dest.Material, opt => opt.Ignore())
            .ForMember(dest => dest.Albaran, opt => opt.Ignore());

        // Mapeo de UsuarioObra
        CreateMap<UsuarioObra, UsuarioObraDto>()
            .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.Name))
            .ForMember(dest => dest.NombreObra, opt => opt.MapFrom(src => src.Obra.Nombre))
            .ReverseMap()
            .ForMember(dest => dest.Usuario, opt => opt.Ignore())
            .ForMember(dest => dest.Obra, opt => opt.Ignore());

        // Mapeo de ParteAveriaDto -> ParteAveria
        CreateMap<ParteAveriaDto, ParteAveria>()
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => Enum.Parse<EstadoAveria>(src.Estado)));

        // Mapeo de ParteAveria -> ParteAveriaDto (con nombres)
        CreateMap<ParteAveria, ParteAveriaDto>()
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.ToString()))
            .ForMember(dest => dest.NombreObra, opt => opt.MapFrom(src => src.Obra != null ? src.Obra.Nombre : ""))
            .ForMember(dest => dest.NombreEmpleado, opt => opt.MapFrom(src => src.Empleado != null ? src.Empleado.Name : ""));
    }
}
