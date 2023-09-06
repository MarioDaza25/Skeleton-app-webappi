

namespace API.Dtos;

public class CiudadPersDto
{
    public int Id { get; set; }
    public string NombreCiudad { get; set; }
    public int IdDepartamentoFk { get; set; }
    public List<PersonaDto> Personas { get; set; }
}
