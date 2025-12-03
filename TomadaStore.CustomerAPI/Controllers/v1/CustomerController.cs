using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.CustomerAPI.Services;
using TomadaStore.CustomerAPI.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region ServiceLoggerDependencyInjection
        private readonly ILogger<CustomerController> _logger;

        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            this._logger = logger;
            this._customerService = customerService;
        }
        #endregion


        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerRequestDTO customer)
        {
            try
            {
                _logger.LogInformation("Creating a new customer.");
                await _customerService.InsertCustomerAsync(customer);

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred while creating a new customer." + ex.Message);
                return Problem(ex.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResponseDTO>>> GetAllCustomersAsync()
        {
            try
            {
                _logger.LogInformation("Catching all customers...");
                var customers = await _customerService.GetAllCustomersAsync();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred while creating a new customer." + ex.Message);
                return Problem(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDTO?>> GetCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Catching customer by ID...");
                var customer = await _customerService.GetCustomerByIdAsync(id);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocurred while creating a new customer." + ex.Message);
                return Problem(ex.Message);
            }

        }

    }
}
