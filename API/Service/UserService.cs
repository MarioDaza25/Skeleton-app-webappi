using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Dtos;
using API.Helpers;
using Aplicacion.Contratos;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Service;

public class UserService : IUserService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Persona> _passwordHasher;
    private readonly IJwtGenerador _jwtGenerador;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<Persona> passwordHasher, IJwtGenerador jwtGenerador)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtGenerador = jwtGenerador;
        
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

                return $"El Usuario {registerDto.Username} Ha Sido Registrado Exitosamente";
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
    
    public async Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
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
            datosUsuarioDto.UserName = persona.Username;
            datosUsuarioDto.Email = persona.Email;
            datosUsuarioDto.Token = _jwtGenerador.CrearToken(persona);
            datosUsuarioDto.Roles = persona.Roles
                                            .Select(u => u.Nombre)
                                            .ToList();
            return datosUsuarioDto;

        }
        datosUsuarioDto.EstaAutenticado = false;
        datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {persona.Username}.";
        return datosUsuarioDto;

    }  

    private JwtSecurityToken CreateJwtToken(Persona persona)
    {
        var roles = persona.Roles;
        var roleClaims = new List<Claim>();
        foreach (var rol in roles)
        {
            roleClaims.Add(new Claim("roles", rol.Nombre));
        }
        var claims = new[]
        {
                    new Claim(JwtRegisteredClaimNames.Sub, persona.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, persona.Email),
                    new Claim("uid", persona.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.HasKey));
        Console.WriteLine("", symmetricSecurityKey);
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }


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

    public async Task<LoginDto> UserLogin(LoginDto model)
    {
        var persona = await _unitOfWork.Personas.GetByUsernameAsync(model.Username);
        var resultado = _passwordHasher.VerifyHashedPassword(persona, persona.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            return model;
        }
        return null;
    }
}
