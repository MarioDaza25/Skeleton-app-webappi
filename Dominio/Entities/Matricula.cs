namespace Dominio.Entities;

public class Matricula : BaseEntity
{
    public string IdPersonaFk { get; set; }
    public Persona Persona { get; set; }
    public string IdSalonFk { get; set; }
    public Salon Salon { get; set; }
    
}
