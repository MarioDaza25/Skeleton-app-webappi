namespace API.Dtos;

public class PaisxDepDto
{
    public int Id { get; set; }
    public string NombrePais { get; set; }
    public List<DepxCiudadDto> Departamentos { get; set; }
    

}
