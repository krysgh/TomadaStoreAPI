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
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomerAsync([FromBody] CustomerRequestDTO customer)
        {
            try
            {
                _logger.LogInformation("Creating a new Customer.");
                await _customerService.InsertCustomerAsync(customer);

                return Created();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while creating a new Customer. " + e.Message);
                return Problem(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResponseDTO>>> GetAllCustomerAsync()
        {
            try
            {
                var customers = await _customerService.GetAllCustomerAsync();

                return Ok(customers);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while retriving all customers" + e.Message);
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerByIdAsync(string id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);

                if (customer is null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while retriving customer" + e.Message);
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> UpdateCustomerStatusByIdAsync(string id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);

                if (customer is null)
                    return NotFound();

                await _customerService.UpdateCustomerStatusByIdAsync(id);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while retriving customer" + e.Message);
                return Problem(e.Message);
            }
        }

    }
}
