using Inmobiliaria.Domain.Enum;

namespace Inmobiliaria.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } =  string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
    
    public string? RefreshToken { get; set; } 
    public DateTime RefreshTokenExpires { get; set; }
    
}