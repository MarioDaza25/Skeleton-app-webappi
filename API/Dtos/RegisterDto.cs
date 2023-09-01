using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RegisterDto
    {
        public string IdPersona { get; }
        public string Nombre { get; }
        public string Apellido { get; }
        public string ApellidoPaterno { get; }
        public string ApellidoMaterno { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public int IdGeneroFk { get; }
        public int IdTPerFk { get; }
        public int IdCiudadFk { get; }
    
    }
}