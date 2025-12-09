using MongoDB.Bson;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.ProductAPI.Repositories.Interfaces;
using TomadaStore.ProductAPI.Services.Interfaces;

namespace TomadaStore.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;

        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(ProductRequestDTO productDTO)
        {
            try
            {
                await _productRepository.CreateProductAsync(productDTO);
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating product: " + e.Message);
            }
        }

        public Task DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(ObjectId id)
        {
            try
            {
                return await _productRepository.GetProductByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retriving product: " + e.Message);
                throw;
            }
        }

        public Task UpdateProductAsync(string id, ProductRequestDTO productDTO)
        {
            throw new NotImplementedException();
        }
    }
}
