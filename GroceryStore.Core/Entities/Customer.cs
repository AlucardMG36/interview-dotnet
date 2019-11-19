using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GroceryStore.Core.Entities
{
    public class Customer: IEquatable<Customer>
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
            throw new NotImplementedException();
        }
    }
}
