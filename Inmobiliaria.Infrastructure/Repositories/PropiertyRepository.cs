using Inmobiliaria.Domain.Interfaces;
using Inmobiliaria.Domain.Models;
using Inmobiliaria.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Infrastructure.Repositories;

public class PropiertyRepository : IPropiertyRepository
{
    private readonly AppDbContext _context;
    
    public PropiertyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Propierty>> GetAllPropierties()
    {
        return await _context.Propierties.ToListAsync();
    }

    public async Task<Propierty?> GetPropiertyById(int id)
    {
        return await _context.Propierties.FindAsync(id);
    }

    public async  Task CreatePropierty(Propierty propierty)
    {
        _context.Propierties.Add(propierty);
        await _context.SaveChangesAsync();
    }

    public async  Task UpdatePropierty(Propierty propierty)
    {
        _context.Propierties.Update(propierty);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePropierty(int id)
    {
        var propierty = await _context.Propierties.FindAsync(id);
        if (propierty != null)
        {
            _context.Propierties.Remove(propierty);
            await _context.SaveChangesAsync();
        }
       
    }
}