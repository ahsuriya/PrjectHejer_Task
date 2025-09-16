using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjectHejer.Application.Enums;
using PrjectHejer.Application.Interfaces.Services;

namespace PrjectHejer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerImagesController(IEntityImageService imageService,
        ILogger<CustomerImagesController> logger) : PHControllerBase
    {
        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> GetCustomerImages(Guid customerId)
        {
            var images = await imageService.GetImagesAsync(customerId, EntityTypeEnum.Customer);
            if (images == null || !images.Any())
                return NotFoundResponse("No images found for the customer.");

            return OkResponse(images, "Customer images retrieved successfully.");
        }

        [HttpPost("{customerId:guid}")]
        public async Task<IActionResult> UploadCustomerImages(Guid customerId, [FromBody] List<string> images)
        {
            try
            {
                var uploaded = await imageService.UploadImagesAsync(customerId, EntityTypeEnum.Customer, images);
                return OkResponse(uploaded, "Customer images uploaded successfully.");
            }
            catch (ArgumentException ex)
            {
                logger.LogWarning(ex, "Upload failed for Customer {CustomerId}", customerId);
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpDelete("{imageId:guid}")]
        public async Task<IActionResult> DeleteImage(Guid imageId)
        {
            try
            {
                var deleted = await imageService.DeleteImageAsync(imageId);
                return DeletedResponse("Image deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                logger.LogWarning(ex, "Delete failed for Image {ImageId}", imageId);
                return NotFoundResponse(ex.Message);
            }
        }
    }
}
