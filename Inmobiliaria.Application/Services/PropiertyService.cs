using Inmobiliaria.Application.DTOs.Propierty;
using Inmobiliaria.Application.Interfaces;
using Inmobiliaria.Domain.Interfaces;
using Inmobiliaria.Domain.Models;

namespace Inmobiliaria.Application.Services;

public class PropiertyService : IPropiertyService
{
    private readonly IPropiertyRepository _repo;

    public PropiertyService(IPropiertyRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<PropiertyRenponse>> GetAll()
    {
        var list = await _repo.GetAllPropierties();

        return list.Select(p => new PropiertyRenponse()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            Location = p.Location,
            ImagesUrl = p.ImagesUrls
        });
    }

    public async Task<PropiertyRenponse?> GetById(int id)
    {
        var p = await _repo.GetPropiertyById(id);
        if (p == null) return null;

        return new PropiertyRenponse()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            Location = p.Location,
            ImagesUrl = p.ImagesUrls
        };
    }

    public async Task<string> CreatePropierty(PropiertyCreateRequest dto)
    {
        var entity = new Propierty()
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Location = dto.Location,
            ImagesUrls = dto.ImagesUrl
        };
            
        await _repo.CreatePropierty(entity);
        return "Propierty Created";

    }
    
    public async Task<string> UpdatePropierty(PropiertyUpdateRequest dto)
    {
        var entity = new Propierty()
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Location = dto.Location,
            ImagesUrls = dto.ImagesUrl
        };

        await _repo.UpdatePropierty(entity);
        return "Propiedad actualizada";
    }

    

    public async Task<string> DeletePropierty(int id)
    {
        await _repo.DeletePropierty(id);
        return "Propiedad eliminada";
    }
}