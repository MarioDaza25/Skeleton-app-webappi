using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
    {
    private readonly IncidenciasContext _context;

    public DepartamentoRepository(IncidenciasContext context) : base(context)
    {
        _context = context;
    }
        
    }