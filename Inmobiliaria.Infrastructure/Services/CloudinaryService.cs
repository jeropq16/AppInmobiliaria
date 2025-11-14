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

        // SUBIR UNA SOLA IMAGEN
        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "File is empty";

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "inmobiliaria" // Carpeta en Cloudinary
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }

        // SUBIR UNA LISTA DE IMÁGENES
        public async Task<List<string>> UploadImages(List<IFormFile> files)
        {
            var urls = new List<string>();

            foreach (var file in files)
            {
                var url = await UploadImage(file);
                urls.Add(url);
            }

            return urls;
        }
    }
}