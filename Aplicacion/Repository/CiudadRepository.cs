using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class CiudadRepository : GenericRepository<Ciudad>, ICiudad
{
    private readonly IncidenciasContext _context;

    public CiudadRepository(IncidenciasContext context) : base(context)
    {
        _context = context;
    }
}
