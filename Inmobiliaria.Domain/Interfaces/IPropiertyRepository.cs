using Inmobiliaria.Domain.Models;

namespace Inmobiliaria.Domain.Interfaces;

public interface IPropiertyRepository
{
    Task<IEnumerable<Propierty>> GetAllPropierties();
    Task<Propierty?> GetPropiertyById(int id);
    Task CreatePropierty(Propierty propierty);
    Task UpdatePropierty(Propierty propierty);
    Task DeletePropierty(int id);
}