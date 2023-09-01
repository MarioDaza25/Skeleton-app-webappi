namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    IPais Paises { get; }
    IPersona Personas { get; }
    IRol Roles { get; }
    Task<int> SaveAsync();
}
