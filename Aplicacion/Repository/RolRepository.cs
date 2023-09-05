using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly IncidenciasContext _context;

    public RolRepository(IncidenciasContext context) : base(context)
    {
        _context = context;
    }

    
}
