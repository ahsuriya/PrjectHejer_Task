using PrjectHejer.DataAccess.Models;
using PrjectHejer.Domain.DTOs.Customer;

namespace PrjectHejer.Application.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<CustomerWithImagesResponse> GetByIdAsync(Guid customerId);
    }
}
