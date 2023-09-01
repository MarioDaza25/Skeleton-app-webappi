using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IPersona : IGenericRepository<Persona>
{
    Task<Persona> GetByUsernameAsync(string username);
}
