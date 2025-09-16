using Microsoft.Extensions.Logging;
using PrjectHejer.Application.Interfaces.Repositories;
using PrjectHejer.DataAccess.Models;
using PrjectHejer.DataAccess;
using Microsoft.EntityFrameworkCore;
using PrjectHejer.Domain.DTOs.Customer;
using PrjectHejer.Domain.DTOs;

namespace PrjectHejer.Infrastructure.Repositories;

public class CustomerRepository(ILogger<CustomerRepository> logger, AppDbContext dbContext)
 : ICustomerRepository
{
    public async Task<List<Customer>> GetAllAsync()
    {
        logger.LogInformation("Querying all customers from database");
        return await dbContext.Customers.AsNoTracking().ToListAsync();
    }
    public async Task<CustomerWithImagesResponse> GetByIdAsync(Guid customerId)
    {
        logger.LogInformation("Querying customer with ID: {CustomerId}", customerId);
        var customer = await dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == customerId);

        var images = await dbContext.EntityImages
            .Where(i => i.EntityId == customerId) // Filter the EntityImages by EntityId  
            .ToListAsync();

        return new CustomerWithImagesResponse { 
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            Images = images.Select(i => new ImageResponse
            {
                Id = i.Id,
                Base64Image = i.Base64Image
            }).ToList()
        };
    }
}
