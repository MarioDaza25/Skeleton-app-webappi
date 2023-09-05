namespace Dominio.Entities;

public class Persona : BaseEntity
{
    public string IdPersona { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    
    
    
    public int IdGeneroFk { get; set; }
    public Genero Genero { get; set; }
    public int IdTPerFk { get; set; }
    public TipoPersona TipoPersona { get; set; }
    public int IdCiudadFk { get; set; }
    public Ciudad Ciudad { get; set; }
    

    public ICollection<TrainerSalon> TrainerSalones { get; set; }
    public ICollection<Salon> Salones { get; set; } = new HashSet<Salon>();
    public ICollection<Matricula> Matriculas { get; set; }
    
    


}
