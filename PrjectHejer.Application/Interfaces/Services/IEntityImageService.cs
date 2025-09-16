using PrjectHejer.Domain.DTOs;

namespace PrjectHejer.Application.Interfaces.Services
{
    public interface IEntityImageService
    {
        Task<List<ImageResponse>> UploadImagesAsync(Guid entityId, Guid entityTypeId, List<string> base64Images);
        Task<List<ImageResponse>> GetImagesAsync(Guid entityId, Guid entityTypeId);
        Task<bool> DeleteImageAsync(Guid imageId);
    }
}
