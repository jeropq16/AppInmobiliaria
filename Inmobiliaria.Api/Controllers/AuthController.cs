using Inmobiliaria.Application.DTOs.User;
using Inmobiliaria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Registro
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest dto)
    {
        var result = await _authService.Register(dto);

        if (result.Contains("registrado"))
            return Ok(result);

        return BadRequest(result);
    }

    // Login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest dto)
    {
        var result = await _authService.Login(dto);

        if (result.StartsWith("ACCESS"))
            return Ok(result);

        return BadRequest(result);
    }

    // Refresh Token
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var result = await _authService.RefreshToken(refreshToken);

        if (result.StartsWith("ACCESS"))
            return Ok(result);

        return BadRequest(result);
    }
}