using Inmobiliaria.Application.DTOs.User;

namespace Inmobiliaria.Application.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginRequest dto);
    Task<string> Register(RegisterRequest dto);
    Task<string> RefreshToken(string refreshToken);
}