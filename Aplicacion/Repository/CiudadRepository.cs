using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class CiudadRepository : GenericRepository<Ciudad>, ICiudad
{
    private readonly IncidenciasContext _context;

    public CiudadRepository(IncidenciasContext context) : base(context)
    {
        _context = context;
    }

     public override async Task<IEnumerable<Ciudad>> GetAllAsync()
        {
            return await _context.Ciudades
                    .Include(p => p.Personas)
                    .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Ciudad> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Ciudades as IQueryable<Ciudad>;
                if(!string.IsNullOrEmpty(search))
                {
                query = query.Where(p => p.NombreCiudad.ToLower().Contains(search));
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

        public override async Task<Ciudad> GetByIdAsync(int Id)
        {
            return await _context.Ciudades
                        .Include(p => p.Personas)
                        .FirstOrDefaultAsync(p => p.Id == Id);
        }
}
