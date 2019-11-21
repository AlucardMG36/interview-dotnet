using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Core.Entities
{
    public class OrderItem
    {
        public OrderItem()
        {
        }

        [JsonIgnore]
        public Product Product { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public decimal Cost { get => (Product?.Price ?? 0) * Quantity; }

        public string MapToJson()
        {
            return $"'productId': '{ProductId}', 'quantity': '{Quantity}'";
        }

        public override string ToString()
        {
            return $"{Product.Description}(s): {Quantity}";
        }
    }
}
