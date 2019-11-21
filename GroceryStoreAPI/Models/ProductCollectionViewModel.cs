using GroceryStore.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
    public class ProductCollectionViewModel : ViewModelCollection<Product>
    {
        private ProductCollectionViewModel(string selfUrl, IEnumerable<ViewModel<Product>> data)
            : base(selfUrl, data)
        {
        }

        internal static ProductCollectionViewModel From(HttpRequest request, IEnumerable<Product> products)
        {
            var requestPath = request.Path;

            var data = products.Select(x => ProductViewModel.From(request, x));

            var vm = new ProductCollectionViewModel(requestPath, data);

            return vm;
        }
    }
}
