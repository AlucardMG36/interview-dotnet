using Newtonsoft.Json;
using System.Collections.Generic;

namespace GroceryStore.Core.Entities
{
    public sealed class RootObject
    {
        [JsonProperty("customers")]
        public IEnumerable<Customer> Customers { get; set; }

        [JsonProperty("orders")]
        public IEnumerable<Order> Orders { get; set; }

        [JsonProperty("products")]
        public IEnumerable<Product> Products { get; set; }

    }
}
