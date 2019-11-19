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

        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Equals([AllowNull] Product other)
        {
            throw new NotImplementedException();
        }
    }
}
