using PrjectHejer.DataAccess.Models;

namespace PrjectHejer.Application.Interfaces.Repositories;

public interface IEntityImageRepository
{
    Task<List<EntityImage>> GetImagesAsync(Guid entityId, Guid entityTypeId);
    Task<int> GetImageCountAsync(Guid entityId, Guid entityTypeId);
    Task<bool> AddImagesAsync(IEnumerable<EntityImage> images);
    Task<EntityImage?> GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(EntityImage image);
}
