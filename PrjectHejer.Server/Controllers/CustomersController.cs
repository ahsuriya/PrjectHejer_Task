using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjectHejer.Application.Interfaces.Services;

namespace PrjectHejer.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(ICustomerService customerService, ILogger<CustomersController> logger) : PHControllerBase
{
    /// <summary>
    /// Get all customers.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        logger.LogInformation("Received request to fetch all customers.");

        var customers = await customerService.GetCustomersAsync();

        if (customers == null || !customers.Any())
        {
            logger.LogWarning("No customers found in the database.");
            return NotFoundResponse("No customers found.");
        }

        logger.LogInformation("Returning {Count} customers.", customers.Count);
        return OkResponse(customers, "Customers retrieved successfully.");
    }
    [HttpGet]
    [Route("{customerId:guid}")]
    public async Task<IActionResult> GetCustomer(Guid customerId)
    {
        logger.LogInformation("Received request to fetch customer with ID: {CustomerId}", customerId);
        var customer = await customerService.GetCustomerAsync(customerId);
        if (customer == null)
        {
            logger.LogWarning("Customer with ID: {CustomerId} not found.", customerId);
            return NotFoundResponse($"Customer with ID: {customerId} not found.");
        }
        logger.LogInformation("Returning customer with ID: {CustomerId}", customerId);
        return OkResponse(customer, "Customer retrieved successfully.");
    }
}
