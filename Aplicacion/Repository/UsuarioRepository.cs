using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuario
{
    private readonly IncidenciasContext _context;
    public UsuarioRepository(IncidenciasContext context) : base(context)
    {
        _context = context;
    }
    public  async Task<Usuario> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios
                .Include(p => p.Roles)
                .FirstOrDefaultAsync(p => p.Username.ToLower() == username.ToLower());
    }
}
