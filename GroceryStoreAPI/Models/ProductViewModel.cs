using GroceryStore.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
    public class ProductViewModel : ViewModel<Product>
    {
        private ProductViewModel(string selfUrl, Product data)
            : base(selfUrl, data)
        {
        }

        internal static ProductViewModel From(HttpRequest request, Product product)
        {
            var selfUrl = $"{request.Path}/{product.Id}";

            var vm = new ProductViewModel(selfUrl, product);

            return vm;
        }

    }
}
