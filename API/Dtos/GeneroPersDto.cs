using Dominio.Entities;

namespace API.Dtos;

public class GeneroPersDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public List<PersonaDto> Personas { get; set; }
}
