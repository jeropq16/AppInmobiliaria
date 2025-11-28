using Inmobiliaria.Application.DTOs.Propierty;
using Inmobiliaria.Application.Interfaces;
using Inmobiliaria.Domain.Interfaces;
using Inmobiliaria.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Inmobiliaria.Application.Services
{
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

            return list.Select(p => new PropiertyRenponse
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

            return new PropiertyRenponse
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
            try
            {
                List<string> urls = new List<string>();

                if (dto.Images?.Any() == true)
                {
                    urls = await _cloudinary.UploadImagesAsync(dto.Images);
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
                return "Propiedad creada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error al crear propiedad: {ex.Message}";
            }
        }

        public async Task<string> UpdatePropierty(PropiertyUpdateRequest dto)
        {
            var existing = await _repo.GetPropiertyById(dto.Id);
            if (existing == null)
                return "Propiedad no encontrada";

            try
            {
                var urls = existing.ImagesUrls ?? new List<string>();

                if (dto.Images?.Any() == true)
                {
                    var newUrls = await _cloudinary.UploadImagesAsync(dto.Images);
                    urls.AddRange(newUrls.Where(u => !string.IsNullOrWhiteSpace(u)));
                }

                existing.Title = dto.Title;
                existing.Description = dto.Description;
                existing.Price = dto.Price;
                existing.Location = dto.Location;
                existing.ImagesUrls = urls;

                await _repo.UpdatePropierty(existing);
                return "Propiedad actualizada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar propiedad: {ex.Message}";
            }
        }

        public async Task<string> DeletePropierty(int id)
        {
            await _repo.DeletePropierty(id);
            return "Propiedad eliminada correctamente";
        }
    }
}
