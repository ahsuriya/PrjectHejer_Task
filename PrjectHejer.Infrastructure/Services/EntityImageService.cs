using Microsoft.Extensions.Logging;
using PrjectHejer.Application.Interfaces.Repositories;
using PrjectHejer.Application.Interfaces.Services;
using PrjectHejer.DataAccess.Models;
using PrjectHejer.Domain.DTOs;
using PrjectHejer.Utilities;

namespace PrjectHejer.Infrastructure.Services;

public class EntityImageService(ILogger<EntityImageService> logger,
                                IEntityImageRepository entityImageRepository) : IEntityImageService
{
    private const int MaxImages = 10;

    public async Task<List<ImageResponse>> UploadImagesAsync(Guid entityId, Guid entityTypeId, List<string> base64Images)
    {
        if (base64Images == null || base64Images.Count == 0)
        {
            logger.LogWarning("No images provided for Entity {EntityId}", entityId);
            throw new ArgumentException("No images provided for upload.");
        }

        var currentCount = await entityImageRepository.GetImageCountAsync(entityId, entityTypeId);
        if (currentCount + base64Images.Count > MaxImages)
        {
            logger.LogWarning("Entity {EntityId} already has {Count} images. Upload rejected.", entityId, currentCount);
            throw new ArgumentException($"Upload failed. A maximum of {MaxImages} images is allowed per entity.");
        }

        var images = base64Images.Select(img => new EntityImage
        {
            Id = Guid.NewGuid(),
            EntityId = entityId,
            EntityTypeId = entityTypeId,
            Base64Image = ImageOptimizer.ReduceBase64ImageSize(img),
            CreatedAt = DateTime.UtcNow
        }).ToList();

        var added = await entityImageRepository.AddImagesAsync(images);
        if (!added)
        {
            logger.LogError("Failed to persist images for Entity {EntityId}", entityId);
            throw new ArgumentException("Upload failed due to a database error.");
        }

        logger.LogInformation("{Count} images uploaded successfully for Entity {EntityId}", images.Count, entityId);

        return images.Select(i => new ImageResponse
        {
            Id = i.Id,
            Base64Image = i.Base64Image
        }).ToList();
    }

    public async Task<List<ImageResponse>> GetImagesAsync(Guid entityId, Guid entityTypeId)
    {
        var list = await entityImageRepository.GetImagesAsync(entityId, entityTypeId);
        return list.Select(i => new ImageResponse
        {
            Id = i.Id,
            Base64Image = i.Base64Image
        }).ToList();
    }

    public async Task<bool> DeleteImageAsync(Guid imageId)
    {
        var img = await entityImageRepository.GetByIdAsync(imageId);
        if (img == null)
        {
            logger.LogWarning("Delete failed: Image {ImageId} not found", imageId);
            throw new ArgumentException("Image not found or already deleted.");
        }

        return await entityImageRepository.DeleteAsync(img);
    }
}
