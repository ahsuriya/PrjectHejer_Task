using PrjectHejer.Domain.DTOs.Customer;

namespace PrjectHejer.Application.Interfaces.Services;

public interface ICustomerService
{
    Task<List<CustomerReponse>> GetCustomersAsync();
    Task<CustomerWithImagesResponse> GetCustomerAsync(Guid customerId);
}
