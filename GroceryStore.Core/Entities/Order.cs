using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GroceryStore.Core.Entities
{
    public class Order : IEquatable<Order>
    {
        public Order()
        {
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }

        public bool Equals([AllowNull] Order other)
        {
            throw new NotImplementedException();
        }
    }
}
