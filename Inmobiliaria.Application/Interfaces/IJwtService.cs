using Inmobiliaria.Domain.Models;

namespace Inmobiliaria.Application.Interfaces;

public interface IJwtService
{
    string GenerateJwt(User user);
    string GenerateRefreshToken();
}