using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IncidenciasContext context;
    private PaisRepository _paises;
    private PersonaRepository _personas;
    public UnitOfWork(IncidenciasContext _context)
    {
        context = _context;
    }

    public IPais Paises
    {
        get
        {
            if(_paises == null)
            {
                _paises = new PaisRepository(context);
            }
            return _paises;
        }
    }

    public IPersona Personas
    {
        get
        {
            if(_personas == null)
            {
                _personas = new PersonaRepository(context);
            }
            return _personas;
        }
    }

    public IRol Roles => throw new NotImplementedException();

    public int Save()
    {
        return context.SaveChanges();
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }

    
}
