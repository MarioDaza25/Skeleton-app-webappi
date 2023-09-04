using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class PersonaRepository : GenericRepository<Persona>, IPersona
{
    private readonly IncidenciasContext _context;

    public PersonaRepository(IncidenciasContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas
                .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Persona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Personas as IQueryable<Persona>;
        if(!string.IsNullOrEmpty(search))
        {
          query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }
         
        var totalRegistros = await query.CountAsync();
        var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        return (totalRegistros, registros);
    }

    public override async Task<Persona> GetByIdAsync(int Id)
    {
        return await _context.Personas
                    .FirstOrDefaultAsync(p => p.Id == Id);
    }
   
} 