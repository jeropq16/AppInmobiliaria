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
        if (!ModelState.IsValid)
            return BadRequest("Datos incompletos o inválidos.");

        var result = await _authService.Register(dto);

        if (result.Contains("registrado", StringComparison.OrdinalIgnoreCase))
            return Ok(new { message = result });

        return BadRequest(new { error = result });
    }

    // Login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Credenciales inválidas.");

        var result = await _authService.Login(dto);

        if (result.StartsWith("ACCESS"))
            return Ok(new { token = result });

        return BadRequest(new { error = result });
    }

    // Refresh Token
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return BadRequest("Debe enviar un refresh token válido.");

        var result = await _authService.RefreshToken(refreshToken);

        if (result.StartsWith("ACCESS"))
            return Ok(new { token = result });

        return BadRequest(new { error = result });
    }
}