using GroceryStore.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
    public class OrderViewModel : ViewModel<Order>
    {
        private OrderViewModel(string selfUrl, Order data)
            : base(selfUrl, data)
        {
        }

        internal static OrderViewModel From(HttpRequest request, Order order)
        {
            var selfUrl = $"{request.Path}/GetByOrderId?id={order.Id}";

            var vm = new OrderViewModel(selfUrl, order);

            vm.AddLink("customer", $"/Customer/GetCustomerById?id={order.CustomerId}");

            return vm;
        }
    }
}
