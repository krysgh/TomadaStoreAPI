using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; }

        [BsonElement("description")]
        public string Description { get; private set; }

        public Category() { }

        [BsonConstructor]
        public Category(ObjectId id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Category(string name, string description)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            Description = description;
        }
    }
}
