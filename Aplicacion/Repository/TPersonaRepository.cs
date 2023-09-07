using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class TPersonaRepository : GenericRepository<TipoPersona>, ITipoPersona
{
  private readonly IncidenciasContext _context;

  public TPersonaRepository(IncidenciasContext context) : base(context)
  {
      _context = context;
  }

  public override async Task<IEnumerable<TipoPersona>> GetAllAsync()
    {
        return await _context.TipoPersonas
                .Include(p => p.Personas)
                .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<TipoPersona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.TipoPersonas as IQueryable<TipoPersona>;
            if(!string.IsNullOrEmpty(search))
            {
            query = query.Where(p => p.Descripcion.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id); 
            var totalRegistros = await query.CountAsync();
            var registros = await query
                    .Include(p => p.Personas)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            return (totalRegistros, registros);
        }

    public override async Task<TipoPersona> GetByIdAsync(int Id)
    {
        return await _context.TipoPersonas
                    .Include(p => p.Personas)
                    .FirstOrDefaultAsync(p => p.Id == Id);
    }
}