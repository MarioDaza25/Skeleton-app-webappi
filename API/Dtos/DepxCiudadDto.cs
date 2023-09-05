namespace API.Dtos;

public class DepxCiudadDto
{
    public int Id { get; set; }
    public string NombreDepartamento { get; set; }
    public int IdPaisFk { get; set; }
    public List<CiudadDto> Ciudades { get; set; }
}
