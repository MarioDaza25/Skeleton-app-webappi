namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    IPais Paises { get; }
    IDepartamento Departamentos { get; }
    ICiudad Ciudades { get; }
    IPersona Personas { get; }
    IRol Roles { get; }
    IUsuario Usuarios { get; }
    Task<int> SaveAsync();
}
