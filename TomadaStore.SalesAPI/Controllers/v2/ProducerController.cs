using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SalesAPI.Services.Interfaces.v2;

namespace TomadaStore.SalesAPI.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {

        private readonly ILogger<ProducerController> _logger;

        private readonly IProducerService _producerService;


        public ProducerController(ILogger<ProducerController> logger, IProducerService producerService)
        {
            _logger = logger;
            _producerService = producerService;
        }

        [HttpPost]
        public async Task<IActionResult> SaleProducerRequest([FromBody] ProducerDTO sale)
        {
            try
            {
                await _producerService.PublishSaleMessageAsync(sale);

                _logger.LogInformation(" [x] Sent {0}", sale);
                return Ok("Sale message sent to RabbitMQ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending sale message to RabbitMQ");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending sale message");
            }
        }

    }
}
