using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; }

        [BsonElement("description")]
        public string Description { get; private set; }

        [BsonElement("price")]
        public decimal Price { get; private set; }

        [BsonElement("category")]
        public Category Category { get; private set; }

        public Product() { }

        public Product(
            string name,
            string description,
            decimal price,
            Category category
        )
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }
    }
}
