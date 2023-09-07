using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{

    public MappingProfiles() 
    {
        CreateMap<Pais,PaisDto>().ReverseMap();

        CreateMap<Pais,PaisxDepDto>().ReverseMap();
        
        CreateMap<Departamento,DepartamentoDto>().ReverseMap();

        CreateMap<Departamento,DepxCiudadDto>().ReverseMap();

        CreateMap<Ciudad,CiudadDto>().ReverseMap();
        
        CreateMap<Ciudad,CiudadPersDto>().ReverseMap();
        
        CreateMap<Persona,PersonaDto>().ReverseMap();

        CreateMap<Persona,PersonaFullDto>().ReverseMap();

        CreateMap<Genero,GeneroDto>().ReverseMap();

        CreateMap<Genero,GeneroPersDto>().ReverseMap();

        CreateMap<TipoPersona,TipoPersonaDto>().ReverseMap();

        CreateMap<TipoPersona,TipoPxPersonDto>().ReverseMap();

        
    }
}
