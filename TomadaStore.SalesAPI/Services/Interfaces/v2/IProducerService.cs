using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.SalesAPI.Services.Interfaces.v2
{
    public interface IProducerService
    {
        Task PublishSaleMessageAsync(ProducerDTO sale);
    }
}
