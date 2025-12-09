using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;

namespace TomadaStore.Consumer.Repositories.Interfaces.v2
{
    public interface IConsumerRepository
    {
        Task CreateSaleAsync(CustomerResponseDTO customerDTO,
            List<(ProductResponseDTO product,
            int quantity)> items);
    }
}
