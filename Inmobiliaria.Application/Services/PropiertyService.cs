using Inmobiliaria.Application.DTOs.Propierty;
using Inmobiliaria.Application.Interfaces;
using Inmobiliaria.Domain.Interfaces;
using Inmobiliaria.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Inmobiliaria.Application.Services;

public class PropiertyService : IPropiertyService
{
    private readonly IPropiertyRepository _repo;
    private readonly ICloudinaryService _cloudinary;


    public PropiertyService(IPropiertyRepository repo, ICloudinaryService cloudinary)
    {
        _repo = repo;
        _cloudinary = cloudinary;
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
        List<string> urls = new List<string>();

        if (dto.Images != null && dto.Images.Any())
        {
            urls = await _cloudinary.UploadImages(dto.Images);
        }
    
        var entity = new Propierty
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Location = dto.Location,
            ImagesUrls = urls
        };
            
        await _repo.CreatePropierty(entity);
        return "Propierty Created";
    }

    public async Task<string> UpdatePropierty(PropiertyUpdateRequest dto)
    {
        var existing = await _repo.GetPropiertyById(dto.Id);
        if (existing == null)
            return "Propiedad no encontrada";

        var urls = existing.ImagesUrls ?? new List<string>();

        if (dto.Images != null && dto.Images.Any())
        {
            var newUrls = await _cloudinary.UploadImages(dto.Images);

            urls.AddRange(newUrls);
        }

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.Price = dto.Price;
        existing.Location = dto.Location;
        existing.ImagesUrls = urls;

        await _repo.UpdatePropierty(existing);
        return "Propiedad actualizada";
    }

    
    public async Task<string> DeletePropierty(int id)
    {
        await _repo.DeletePropierty(id);
        return "Propiedad eliminada";
    }

}