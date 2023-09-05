namespace API.Dtos;

public class PersonaFullDto
{
    public int Id { get; set; }
    public string IdPersona { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public int IdGeneroFk { get; set; }
    public int IdTPerFk { get; set; }
    public int IdCiudadFk { get; set; }
    
}
