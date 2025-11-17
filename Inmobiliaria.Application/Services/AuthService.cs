using Inmobiliaria.Application.DTOs.User;
using Inmobiliaria.Application.Interfaces;
using Inmobiliaria.Domain.Interfaces;
using Inmobiliaria.Domain.Models;

namespace Inmobiliaria.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IJwtService _jwt;

    public AuthService(IUserRepository userRepo, IJwtService jwt)
    {
        _userRepo = userRepo;
        _jwt = jwt;
    }

    public async Task<string> Register(RegisterRequest dto)
    {
        var existing = await _userRepo.GetUserByEmail(dto.Email);
        if (existing != null)
            return "El email ya existe";

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = "Admin",
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _userRepo.CreateUser(user);
        return "Usuario registrado correctamente";
    }

    public async Task<string> Login(LoginRequest dto)
    {
        var user = await _userRepo.GetUserByEmail(dto.Email);
        if (user == null)
            return "Usuario no encontrado";

        bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
        if (!valid)
            return "Contraseña incorrecta";

        string accessToken = _jwt.GenerateJwt(user);
        string refreshToken = _jwt.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);
        
        await _userRepo.UpdateUser(user);

        

        return $"ACCESS:{accessToken} | REFRESH:{refreshToken}";
    }

    public async Task<string> RefreshToken(string refreshToken)
    {
        var user = await _userRepo.GetUserByRefreshToken(refreshToken);
        if (user == null)
            return "Refresh no válido";

        if (user.RefreshTokenExpires < DateTime.UtcNow)
            return "Refresh expirado";

        string newAccess = _jwt.GenerateJwt(user);
        string newRefresh = _jwt.GenerateRefreshToken();

        user.RefreshToken = newRefresh;
        user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);

        await _userRepo.UpdateUser(user);

        return $"ACCESS:{newAccess} | REFRESH:{newRefresh}";
    }
}