using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;


namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IncidenciasContext context;
    private CiudadRepository _ciudades;
    private DepartamentoRepository _departamentos;
    private GeneroRepository _generos;
    private MatriculaRepository _matriculas;
    private PaisRepository _paises;
    private PersonaRepository _personas;
    private RolRepository _roles;
    private SalonRepository _salones;
    private TPersonaRepository _tpersonas;
    private UsuarioRepository _usuarios;

    public UnitOfWork(IncidenciasContext _context)
    {
        context = _context;
    }


    public ICiudad Ciudades 
    {
        get
        {
            if(_ciudades == null)
            {
                _ciudades = new CiudadRepository(context);
            }
            return _ciudades;
        }
    }

    public IUsuario Usuarios 
    {
        get
        {
            if(_usuarios == null)
            {
                _usuarios = new UsuarioRepository(context);
            }
            return _usuarios;
        }
    }
    public IDepartamento Departamentos 
    {
        get
        {
            if(_departamentos == null)
            {
                _departamentos = new DepartamentoRepository(context);
            }
            return _departamentos;
        }
    }
    public IGenero Generos 
    {
        get
        {
            if(_generos == null)
            {
                _generos = new GeneroRepository(context);
            }
            return _generos;
        }
    }
    public IMatricula Matriculas 
    {
        get
        {
            if(_matriculas == null)
            {
                _matriculas = new MatriculaRepository(context);
            }
            return _matriculas;
        }
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

    public IRol Roles 
    {
        get
        {
            if(_roles == null)
            {
                _roles = new RolRepository(context);
            }
            return _roles;
        }
    }
    public ISalon Salones 
    {
        get
        {
            if(_salones == null)
            {
                _salones = new SalonRepository(context);
            }
            return _salones;
        }
    }

    public ITipoPersona TipoPersonas 
    {
        get
        {
            if(_tpersonas == null)
            {
                _tpersonas = new TPersonaRepository(context);
            }
            return _tpersonas;
        }
    }

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
