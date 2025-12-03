
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
                await _customerRepository.InsertCustomerAsync(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
