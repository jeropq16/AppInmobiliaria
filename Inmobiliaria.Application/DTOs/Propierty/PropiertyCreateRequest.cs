using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Inmobiliaria.Application.DTOs.Propierty;

public class PropiertyCreateRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Location { get; set; } = string.Empty;
    [Required]
    public IFormFile[]?  Images { get; set; }
}