using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class SalonRepository : GenericRepository<Salon>, ISalon
{
  private readonly IncidenciasContext _context;

  public SalonRepository(IncidenciasContext context) : base(context)
  {
      _context = context;
  }   
}