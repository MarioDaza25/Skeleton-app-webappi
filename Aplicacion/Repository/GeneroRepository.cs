using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class GeneroRepository : GenericRepository<Genero>, IGenero
{
    private readonly IncidenciasContext _context;

    public GeneroRepository(IncidenciasContext context) : base(context)
    {
        _context = context;
    }
}
