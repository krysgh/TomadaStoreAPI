using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomadaStore.Models.DTOs.Customer;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleResponseDTO
    {
        [BsonElement("customer")]
        public string CustomerId { get; set; }

        [BsonElement("items")]
        public List<SaleItemDTO> Items { get; init; }

        [BsonElement("saleDate")]
        public DateTime SaleDate { get; private set; }

        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; private set; }
    }
}
