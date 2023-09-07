namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    IPais Paises { get; }
    IDepartamento Departamentos { get; }
    ICiudad Ciudades { get; }
    IPersona Personas { get; }
    IRol Roles { get; }
    IUsuario Usuarios { get; }
    IGenero Generos { get; }
    ITipoPersona TipoPersonas { get;}
    Task<int> SaveAsync();
}
