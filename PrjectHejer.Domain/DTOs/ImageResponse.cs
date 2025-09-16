namespace PrjectHejer.Domain.DTOs;

public class ImageResponse
{
    public Guid Id { get; set; }
    public string Base64Image { get; set; } = string.Empty;
}
