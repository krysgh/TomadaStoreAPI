
using MongoDB.Bson;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SalesAPI.Services.Interfaces.v2;

namespace TomadaStore.SalesAPI.Services.v2
{
    public class ProducerService : IProducerService
    {
        private readonly IConnectionFactory _connectionFactory;


        public ProducerService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task PublishSaleMessageAsync(ProducerDTO sale)
        {
            var saleMessage = JsonSerializer.Serialize(sale);
            var body = Encoding.UTF8.GetBytes(saleMessage);

            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "sales_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            await channel.BasicPublishAsync(exchange: string.Empty,
                                            routingKey: "sales_queue",
                                            body: body);
        }
    }
}
