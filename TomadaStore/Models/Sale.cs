using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.Models
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; private set; }

        [BsonElement("customer")]
        public Customer Customer { get; private set; }

        [BsonElement("products")]
        public List<Product> Products { get; private set; }

        [BsonElement("saleDate")]
        public DateTime SaleDate { get; private set; }

        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; private set; }

        public Sale() { }

        public Sale(
            Customer customer,
            List<Product> products,
            decimal totalPrice
        )
        {
            Id = ObjectId.GenerateNewId();
            Customer = customer;
            Products = products;
            SaleDate = DateTime.UtcNow;
            TotalPrice = totalPrice;
        }
    }
}
