using Inmobiliaria.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public  AppDbContext(DbContextOptions <AppDbContext> options) : base(options)
    {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Propierty> Propierties { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Propierty>()
            .Property(p => p.ImagesUrls)
            .HasConversion(
                v => string.Join(",", v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
    }
}