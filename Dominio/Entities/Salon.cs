namespace Dominio.Entities;

public class Salon : BaseEntity
{
    public string Nombresalon { get; set; }
    public int Capacidad { get; set; }
    public ICollection<Persona> Personas { get; set; } = new HashSet<Persona>();
    public ICollection<TrainerSalon> TrainerSalones { get; set; }
    public ICollection<Matricula> Matriculas { get; set; }
}
