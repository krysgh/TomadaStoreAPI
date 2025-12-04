
using TomadaStore.CustomerAPI.Repositories.Interfaces;
using TomadaStore.CustomerAPI.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        #region RepositoryLoggerDependencyInjection
        private readonly ILogger<CustomerService> _logger;

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }
        #endregion

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                var newCustomer = new Customer(customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber);
                await _customerRepository.InsertCustomerAsync(newCustomer);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                return await _customerRepository.GetAllCustomersAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<CustomerResponseDTO?> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _customerRepository.GetCustomerByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task UpdateCustomerStatusByIdAsync(int id)
        {
            try
            {
                await _customerRepository.UpdateCustomerStatusByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

    }
}
