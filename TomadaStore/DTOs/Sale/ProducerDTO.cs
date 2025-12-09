using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomadaStore.Models.DTOs.Sale
{
    public class ProducerDTO
    {
        [BsonElement("customerId")]
        public int CustomerId { get; init; }

        [BsonElement("items")]
        public List<SaleItemDTO> Items { get; init; }
    }
}
