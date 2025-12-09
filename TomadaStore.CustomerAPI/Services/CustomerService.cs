using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TomadaStore.CustomerAPI.Repository;
using TomadaStore.CustomerAPI.Repository.Interfaces;
using TomadaStore.CustomerAPI.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomerAsync()
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

        public async Task<CustomerResponseDTO?> GetCustomerByIdAsync(string id)
        {
            try
            { 

                return await _customerRepository.GetCustomerByIdAsync(Int32.Parse(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName))
                _logger.LogInformation("FirstName is a required field!");

            if (string.IsNullOrEmpty(customer.LastName))
                _logger.LogInformation("LastName is a required field!");

            if (string.IsNullOrEmpty(customer.Email))
                _logger.LogInformation("Email is a required field!");

            var newCustomer = new Customer(customer.FirstName,
                                           customer.LastName,
                                           customer.Email,
                                           customer.PhoneNumber);
            try
            {
                await _customerRepository.InsertCustomerAsync(newCustomer);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateCustomerStatusByIdAsync(string id)
        {
            try
            {
                await _customerRepository.UpdateCustomerStatusByIdAsync(Int32.Parse(id));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
