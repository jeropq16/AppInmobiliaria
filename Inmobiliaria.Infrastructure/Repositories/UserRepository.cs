using Inmobiliaria.Domain.Interfaces;
using Inmobiliaria.Domain.Models;
using Inmobiliaria.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            
    }

    public async  Task<User?> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
    }

    public async Task UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByRefreshToken(string refreshToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }
}