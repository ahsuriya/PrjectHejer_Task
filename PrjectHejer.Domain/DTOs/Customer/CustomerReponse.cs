namespace PrjectHejer.Domain.DTOs.Customer;

public class CustomerReponse
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class CustomerWithImagesResponse
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public List<ImageResponse> Images { get; set; } = [];
}
