using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Consumer.Repositories.Interfaces.v2;
using TomadaStore.Consumer.Services.Interfaces.v2;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace TomadaStore.Consumer.Services.v2
{
    public class ConsumerService : IConsumerService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly IConsumerRepository _consumerRepository;
        private readonly IConnectionFactory _connectionFactory;
        private readonly HttpClient _httpClientCustomer;
        private readonly HttpClient _httpClientProduct;


        public ConsumerService(ILogger<ConsumerService> logger, IConsumerRepository consumerRepository, IConnectionFactory connectionFactory, IHttpClientFactory factory)
        {
            _logger = logger;
            _consumerRepository = consumerRepository;
            _connectionFactory = connectionFactory;
            _httpClientCustomer = factory.CreateClient("CustomerAPI");
            _httpClientProduct = factory.CreateClient("ProductAPI");
        }

        public async Task CreateSaleAsync()
        {

            try
            {
                using var connection = await _connectionFactory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();
                await channel.QueueDeclareAsync(queue: "sales_queue", durable: false, exclusive: false, autoDelete: false,
                    arguments: null);
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var finalSale = JsonSerializer.Deserialize<ProducerDTO>(message);
                    _logger.LogInformation("Received: ", message);

                    var customer = await _httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(finalSale.CustomerId.ToString());

                    var items = new List<(ProductResponseDTO product, int quantity)>();

                    foreach (var item in finalSale.Items)
                    {
                        var product =  await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(item.ProductId);
                        items.Add((product, item.Quantity));
                    }

                     await _consumerRepository.CreateSaleAsync(customer, items);

                };
                var sale = await channel.BasicConsumeAsync("sales_queue", autoAck: true, consumer: consumer);
            }
            catch (Exception ex) 
            {
                _logger.LogInformation(ex.Message);
            }
            

        }
    }
}
