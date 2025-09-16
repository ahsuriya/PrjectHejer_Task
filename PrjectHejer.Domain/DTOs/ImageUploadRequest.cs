namespace PrjectHejer.Domain.DTOs;

public class ImageUploadRequest
{
    public Guid EntityId { get; set; }
    public Guid EntityTypeId { get; set; }
    public string Base64Image { get; set; } = string.Empty;
}
