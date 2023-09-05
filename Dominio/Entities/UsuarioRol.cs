namespace Dominio.Entities;

public class UsuarioRol
{
    public int IdUsuarioFk { get; set; }
    public Usuario Usuario { get; set; }
    public int IdRolFk { get; set; }
    public Rol Rol { get; set; }


}
