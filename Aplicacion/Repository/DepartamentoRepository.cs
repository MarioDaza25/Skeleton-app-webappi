using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
    {
        private readonly IncidenciasContext _context;

        public DepartamentoRepository(IncidenciasContext context) : base(context)
        {
            _context = context;
        }
        
        public override async Task<IEnumerable<Departamento>> GetAllAsync()
        {
            return await _context.Departamentos
                    .Include(p => p.Ciudades)
                    .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Departamento> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Departamentos as IQueryable<Departamento>;
                if(!string.IsNullOrEmpty(search))
                {
                query = query.Where(p => p.NombreDepartamento.ToLower().Contains(search));
                }
                query = query.OrderBy(p => p.Id); 
                var totalRegistros = await query.CountAsync();
                var registros = await query
                        .Include(p => p.Ciudades)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                return (totalRegistros, registros);
            }

        public override async Task<Departamento> GetByIdAsync(int Id)
        {
            return await _context.Departamentos
                        .Include(p => p.Ciudades)
                        .FirstOrDefaultAsync(p => p.Id == Id);
        }

    }