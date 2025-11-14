namespace Inmobiliaria.Domain.Models;

public class Propierty
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } =  string.Empty;
    public double Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public List<string> ImagesUrls { get; set; } = new List<string>();
}