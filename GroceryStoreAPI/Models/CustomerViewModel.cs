using GroceryStore.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
    public sealed class CustomerViewModel: ViewModel<Customer>
    {
        private CustomerViewModel(string selfUrl, Customer data)
            :base(selfUrl, data)
        {
        }

        internal static CustomerViewModel From(HttpRequest request, Customer customer)
        {
            var selfUrl = $"{request.Path}/{customer.Id}";

            var vm = new CustomerViewModel(selfUrl, customer);

            return vm;
        }

    }
}
