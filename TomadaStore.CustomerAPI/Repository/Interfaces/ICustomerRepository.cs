using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task InsertCustomerAsync(Customer customer);
        Task<CustomerResponseDTO?> GetCustomerByIdAsync(int id);
        Task<List<CustomerResponseDTO>> GetAllCustomersAsync();

        Task UpdateCustomerStatusByIdAsync(int id);
    }
}
