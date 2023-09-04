using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class TPersonaRepository : GenericRepository<TipoPersona>, ITipoPersona
{
  private readonly IncidenciasContext _context;

  public TPersonaRepository(IncidenciasContext context) : base(context)
  {
      _context = context;
  }
}