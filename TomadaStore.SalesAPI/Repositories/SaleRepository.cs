using MongoDB.Bson;
using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SalesAPI.Data;
using TomadaStore.SalesAPI.Repositories.Interfaces;

namespace TomadaStore.SaleAPI.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ILogger<SaleRepository> _logger;

        private readonly IMongoCollection<Sale> _mongoCollection;

        private readonly ConnectionDB _connection;

        public SaleRepository(
            ILogger<SaleRepository> logger,
            ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _mongoCollection = connection.GetMongoCollection();
        }


        public async Task CreateSaleAsync(
            CustomerResponseDTO customerDTO,
            List<(ProductResponseDTO product,
            int quantity)> items)
        {
            var products = new List<Product>();
            decimal total = 0;

            foreach (var (productDTO, quantity) in items)
            {
                var category = new Category(
                    new ObjectId(productDTO.Category.Id),
                    productDTO.Category.Name,
                    productDTO.Category.Description
                );

                var product = new Product(
                    productDTO.Name,
                    productDTO.Description,
                    productDTO.Price,
                    category
                );

                products.Add(product);

                total += productDTO.Price * quantity;
            }

            var customer = new Customer(
                customerDTO.FirstName,
                customerDTO.LastName,
                customerDTO.Email,
                customerDTO.PhoneNumber
            );

            var sale = new Sale(customer, products, total);

            await _mongoCollection.InsertOneAsync(sale);
        }
    }
}
