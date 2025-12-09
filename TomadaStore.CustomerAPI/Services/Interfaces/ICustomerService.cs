using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task InsertCustomerAsync(CustomerRequestDTO customer);
        Task<List<CustomerResponseDTO>> GetAllCustomerAsync();
        Task<CustomerResponseDTO?> GetCustomerByIdAsync(string id);
        Task UpdateCustomerStatusByIdAsync(string id);
    }
}
