namespace Dominio.Entities;

public class Genero
{
    public string Descripcion { get; set; }
    public ICollection<Persona> Personas { get; set; }
}
