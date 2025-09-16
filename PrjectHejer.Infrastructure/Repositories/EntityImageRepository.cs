using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrjectHejer.Application.Interfaces.Repositories;
using PrjectHejer.DataAccess;
using PrjectHejer.DataAccess.Models;

namespace PrjectHejer.Infrastructure.Repositories
{
    public class EntityImageRepository(ILogger<EntityImageRepository> logger,
        AppDbContext dbContext) : IEntityImageRepository
    {
        public async Task<List<EntityImage>> GetImagesAsync(Guid entityId, Guid entityTypeId)
        {
            logger.LogInformation("Querying images for Entity {EntityId} of Type {EntityTypeId}", entityId, entityTypeId);

            return await dbContext.EntityImages
                .Where(i => i.EntityId == entityId && i.EntityTypeId == entityTypeId)
                .ToListAsync();
        }

        public async Task<int> GetImageCountAsync(Guid entityId, Guid entityTypeId)
        {
            var count = await dbContext.EntityImages
                .CountAsync(i => i.EntityId == entityId && i.EntityTypeId == entityTypeId);

            logger.LogInformation("Entity {EntityId} currently has {Count} images", entityId, count);
            return count;
        }

        public async Task<bool> AddImagesAsync(IEnumerable<EntityImage> images)
        {
            if (images == null || !images.Any())
            {
                logger.LogWarning("No images provided to AddImagesAsync");
                return false;
            }

            await dbContext.EntityImages.AddRangeAsync(images);
            var changes = await dbContext.SaveChangesAsync();

            logger.LogInformation("{Count} images added successfully", changes);
            return changes > 0;
        }

        public async Task<EntityImage?> GetByIdAsync(Guid id)
        {
            logger.LogInformation("Fetching image {ImageId}", id);
            return await dbContext.EntityImages.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> DeleteAsync(EntityImage image)
        {
            if (image == null)
            {
                logger.LogWarning("DeleteAsync called with null image");
                return false;
            }

            logger.LogInformation("Removing image {ImageId}", image.Id);
            dbContext.EntityImages.Remove(image);

            var changes = await dbContext.SaveChangesAsync();
            logger.LogInformation("Image {ImageId} delete result: {Success}", image.Id, changes > 0);

            return changes > 0;
        }
    }
}
