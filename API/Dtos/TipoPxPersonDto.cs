namespace API.Dtos;

public class TipoPxPersonDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public ICollection<PersonaDto> Personas { get; set; }
}
