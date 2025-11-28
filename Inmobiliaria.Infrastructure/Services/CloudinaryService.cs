using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Inmobiliaria.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Inmobiliaria.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        // SUBIR UNA SOLA IMAGEN — CORREGIDO
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("El archivo recibido está vacío.");

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "inmobiliaria"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception($"Cloudinary error: {uploadResult.Error.Message}");

            return uploadResult.SecureUrl.ToString();
        }

        // SUBIR VARIAS IMÁGENES — CORREGIDO
        public async Task<List<string>> UploadImagesAsync(IEnumerable<IFormFile> files)
        {
            var urls = new List<string>();

            foreach (var file in files)
            {
                var url = await UploadImageAsync(file);
                urls.Add(url);
            }

            return urls;
        }
    }
}