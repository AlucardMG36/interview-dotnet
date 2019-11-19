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

        public Product Product { get; set; }

        public int Quantity { get; set; }


        public override string ToString()
        {
            return $"{Product.Description}(s): {Quantity}";
        }
    }
}
