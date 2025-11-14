namespace Inmobiliaria.Application.DTOs.Propierty;

public class PropiertyUpdateRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Location { get; set; } = string.Empty;

    public List<string> ImagesUrl { get; set; } = new List<string>();
}