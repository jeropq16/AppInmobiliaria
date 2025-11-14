namespace Inmobiliaria.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } =  string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Client";
    
    public string? RefreshToken { get; set; } 
    public DateTime RefreshTokenExpires { get; set; }
    
}