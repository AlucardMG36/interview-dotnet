using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GroceryStore.Core.Entities
{
    public class Product: IEquatable<Product>
    {

        public Product()
        {
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        public bool Equals([AllowNull] Product other)
        {
            if(other is null)
            {
                return false;
            }

            if(Id == other.Id)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if(obj is null)
            {
                return false;
            }

            return obj is Product other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public string ToJsonString()
        {
            return $"{{'id':'{Id}', 'description': '{Description}', 'price': '{Price}'}}";
        }

        public override string ToString()
        {
            return ToJsonString();
        }
    }
}
