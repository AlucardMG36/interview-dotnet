using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GroceryStore.Core.Entities
{
    public class Customer : IEquatable<Customer>
    {
        public Customer()
        {
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public bool Equals([AllowNull] Customer other)
        {
            if(other is null)
            {
                return false;
            }

            if(other.Id == Id)
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
            return (obj is Customer other) && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return ToJsonString();
        }

        public string ToJsonString()
        {
            return $"{{'id': {Id}, 'name': '{Name}'}}";
        }
    }
}
