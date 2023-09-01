namespace Dominio.Entities;

public class Rol : BaseEntity
{
    public string Nombre { get; set; }
    public ICollection<Persona> Personas { get; set; } = new HashSet<Persona>();
    public ICollection<PersonaRol> PersonaRoles { get; set; }
}
