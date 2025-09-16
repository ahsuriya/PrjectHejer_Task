using Microsoft.Extensions.Logging;
using PrjectHejer.Application.Interfaces.Repositories;
using PrjectHejer.Application.Interfaces.Services;
using PrjectHejer.Domain.DTOs.Customer;

namespace PrjectHejer.Infrastructure.Services;

public class CustomerService(ILogger<CustomerService> logger, ICustomerRepository repository) : ICustomerService
{
    public async Task<List<CustomerReponse>> GetCustomersAsync()
    {
        logger.LogInformation("Fetching all customers");

        var customers = await repository.GetAllAsync();

        if (customers == null || !customers.Any())
        {
            logger.LogWarning("No customers found in database");
            return new List<CustomerReponse>();
        }

        return customers.Select(c => new CustomerReponse
        {
            CustomerId = c.CustomerId,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone
        }).ToList();
    }
    public async Task<CustomerWithImagesResponse> GetCustomerAsync(Guid customerId)
    {
        logger.LogInformation("Fetching customer with ID: {CustomerId}", customerId);
        var customerWithImages = await repository.GetByIdAsync(customerId);
        if (customerWithImages == null)
        {
            logger.LogWarning("Customer with ID: {CustomerId} not found", customerId);
            return null;
        }
        return new CustomerWithImagesResponse
        {
            CustomerId = customerWithImages.CustomerId,
            Name = customerWithImages.Name,
            Email = customerWithImages.Email,
            Phone = customerWithImages.Phone,
            Images = customerWithImages.Images,
        };
    }
}
