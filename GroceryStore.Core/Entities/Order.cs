using GroceryStore.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GroceryStore.Core.Entities
{
    public class Order : IEquatable<Order>
    {
        public Order()
        {
        }

        [JsonIgnore]
        public Customer Customer { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("items")]
        public IEnumerable<OrderItem> Items { get; set; }

        public bool Equals([AllowNull] Order other)
        {
            if(other is null)
            {
                return false;
            }

            if(other.Id == Id && other.CustomerId == CustomerId)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return (obj is Order other) && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private string ToJson()
        {
            var jsonOrderItems = JsonHelper.FromClass(Items);

            return $"{{'id':'{Id}', 'customerId':'{CustomerId}','items': {jsonOrderItems}}}";
        }

        public override string ToString()
        {
            return ToJson();
        }
    }
}
