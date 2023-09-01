using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class PersonaRepository : GenericRepository<Persona>, IPersona
{
  private readonly IncidenciasContext _context;

  public PersonaRepository(IncidenciasContext context) : base(context)
  {
      _context = context;
  }

    public Task<Persona> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
} 