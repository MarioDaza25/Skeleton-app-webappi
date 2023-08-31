namespace Dominio.Entities;
public class Departamento : BaseEntity
{
    public string NombreDepartamento { get; set; }
    public int IdPaisFk { get; set; }
    public Pais Pais { get; set; }
    public ICollection<Ciudad> Ciudades { get; set; }
}
