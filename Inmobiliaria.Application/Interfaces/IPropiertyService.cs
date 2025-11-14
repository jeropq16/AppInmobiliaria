using Inmobiliaria.Application.DTOs.Propierty;
using Inmobiliaria.Domain.Models;

namespace Inmobiliaria.Application.Interfaces;

public interface IPropiertyService
{
    Task<IEnumerable<PropiertyRenponse>> GetAll();
    Task<PropiertyRenponse> GetById(int id);
    Task<string> CreatePropierty(PropiertyCreateRequest dto);
    Task<string> UpdatePropierty(PropiertyUpdateRequest dto);
    Task<string> DeletePropierty(int id);
}