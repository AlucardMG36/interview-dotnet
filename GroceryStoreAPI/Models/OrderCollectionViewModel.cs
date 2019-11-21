using GroceryStore.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Models
{
    public class OrderCollectionViewModel: ViewModelCollection<Order>
    {
        private OrderCollectionViewModel(string selfUrl, IEnumerable<ViewModel<Order>> data)
            : base(selfUrl, data)
        {
        }

        internal static OrderCollectionViewModel From(HttpRequest request, IEnumerable<Order> orders)
        {
            var requestPath = request.Path;

            var data = orders.Select(x => OrderViewModel.From(request, x));

            var vm = new OrderCollectionViewModel(requestPath, data);

            vm.AddLink("add", "/Order/AddOrder");

            return vm;
        }
    }
}
