using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class MatriculaRepository : GenericRepository<Matricula>, IMatricula
{
    private readonly IncidenciasContext _context;

    public MatriculaRepository(IncidenciasContext context) : base(context)
    {
        _context = context;
    }
}