
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using TomadaStore.Consumer.Services.Interfaces.v2;
using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.Consumer.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly ILogger<ConsumerController> _logger;

        private readonly IConnectionFactory _connectionFactory;

        private readonly IConsumerService _consumerService;

        public ConsumerController(ILogger<ConsumerController> logger, IConnectionFactory connectionFactory, IConsumerService consumerService)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            _consumerService = consumerService;
        }

        [HttpPost]
        public async Task<ActionResult> SaleConsumer()
        {
            try
            {
                await _consumerService.CreateSaleAsync();
                return Ok("Consuming sales...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending sale message to RabbitMQ");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending sale message");
            }
        }

    }
}
