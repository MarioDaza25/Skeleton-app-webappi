using System.IdentityModel.Tokens.Jwt;
using API.Dtos;
using API.Helpers;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace API.Service;

public class UserService : IUserService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Persona> _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<Persona> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var persona = new Persona
        {
            IdPersona = registerDto.IdPersona,
            Nombre = registerDto.Nombre,
            Apellido = registerDto.Apellido,
            ApellidoMaterno = registerDto.ApellidoMaterno,
            ApellidoPaterno = registerDto.ApellidoPaterno,
            Email = registerDto.Email,
            Username = registerDto.Username,
            IdGeneroFk = registerDto.IdGeneroFk,
            IdCiudadFk = registerDto.IdCiudadFk,
            IdTPerFk = registerDto.IdTPerFk,
        };

        persona.Password = _passwordHasher.HashPassword(persona, registerDto.Password);

        var usuarioExiste = _unitOfWork.Personas
                        .Find(u => u.Username.ToLower() == registerDto.Username.ToLower())
                        .FirstOrDefault();

        if (usuarioExiste == null)
        {
            var rolPredeterminado = _unitOfWork.Roles
                        .Find(u => u.Nombre == Autorizacion.rol_predeterminado.ToString())
                        .First();
            try
            {
                persona.Roles.Add(rolPredeterminado);
                _unitOfWork.Personas.Add(persona);
                await _unitOfWork.SaveAsync();

                return $"EL Usuario {registerDto.Username} Ha Sido Registrado Exitosamente";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"EL Usuario {registerDto.Username} Ya Se Encuentra Registrado ";
        }                
    }
    
    /* public async Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
    {
        DatosUsuarioDto datosUsuarioDto = new DatosUsuarioDto();
        var persona = await _unitOfWork.Personas
                    .GetByUsernameAsync(model.Username);

        if (persona == null)
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"No existe ningún usuario con el username {model.Username}.";
            return datosUsuarioDto;
        }

        var resultado = _passwordHasher.VerifyHashedPassword(persona, persona.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {

            datosUsuarioDto.EstaAutenticado = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(persona);
            datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            datosUsuarioDto.Email = persona.Email;
            datosUsuarioDto.UserName = persona.Username;
            datosUsuarioDto.Roles = persona.Roles
                                            .Select(u => u.Nombre)
                                            .ToList();
            return datosUsuarioDto;

        }
        datosUsuarioDto.EstaAutenticado = false;
        datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {persona.Username}.";
        return datosUsuarioDto;

    }  */


    public async Task<string> AddRolAsync(AddRolesDto model)
    {
        var persona = await _unitOfWork.Personas
                    .GetByUsernameAsync(model.Username);


        if (persona == null)
        {
            return $"No existe algún usuario registrado con la cuenta {model.Username}.";
        }


        var resultado = _passwordHasher.VerifyHashedPassword(persona, persona.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            var rolExiste = _unitOfWork.Roles
                            .Find(u => u.Nombre.ToLower() == model.Roles.ToLower())
                            .FirstOrDefault();


            if (rolExiste != null)
            {
                var usuarioTieneRol = persona.Roles
                                    .Any(u => u.Id == rolExiste.Id);

                if (usuarioTieneRol == false)
                {
                    persona.Roles.Add(rolExiste);
                    _unitOfWork.Personas.Update(persona);
                    await _unitOfWork.SaveAsync();
                }

                return $"Rol {model.Roles} agregado a la cuenta {model.Username} de forma exitosa.";
            }

            return $"Rol {model.Roles} no encontrado.";
        }
        return $"Credenciales incorrectas para el usuario {persona.Username}.";
    }

    public Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
    {
        throw new NotImplementedException();
    }
}
