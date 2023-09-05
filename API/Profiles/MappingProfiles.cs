using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{

    public MappingProfiles() 
    {
        CreateMap<Pais,PaisDto>().ReverseMap()
        .ForMember(o => o.Departamentos,d=> d.Ignore());

        CreateMap<Pais,PaisxDepDto>().ReverseMap();
        
        CreateMap<Departamento,DepartamentoDto>().ReverseMap();

        CreateMap<Departamento,DepxCiudadDto>().ReverseMap();

        CreateMap<Ciudad,CiudadDto>().ReverseMap();
        
        CreateMap<Persona,PersonaDto>().ReverseMap();

        CreateMap<Persona,PersonaFullDto>().ReverseMap();
        
    }
}
