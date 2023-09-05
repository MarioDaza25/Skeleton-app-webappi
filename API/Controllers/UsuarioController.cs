using System;
using API.Dtos;
using API.Service;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;


public class UsuarioController : BaseAPiController
{
    private readonly IUserService _userService;
    public UsuarioController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await _userService.GetTokenAsync(model);
        return Ok(result);
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRolesDto model)
    {
        var result = await _userService.AddRolAsync(model);
        return Ok(result);
    }
}
