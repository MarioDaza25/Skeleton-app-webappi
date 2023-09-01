namespace Dominio.Entities;

public class PersonaRol
{
    public int IdUsuarioFk { get; set; }
    public Persona Persona { get; set; }
    public int IdRolFk { get; set; }
    public Rol Rol { get; set; }
}
