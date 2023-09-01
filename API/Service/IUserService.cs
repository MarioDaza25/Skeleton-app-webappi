using API.Dtos;

namespace API.Service;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto model);
    Task<DatosUsuarioDto> GetTokenAsync(LoginDto model);
    Task<string> AddRolAsync(AddRolesDto model);
}
