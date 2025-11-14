using Inmobiliaria.Domain.Models;

namespace Inmobiliaria.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(int id);
    Task CreateUser(User user);
    Task<User?> GetUserByRefreshToken(string refreshToken);
    Task UpdateUser(User user);
}