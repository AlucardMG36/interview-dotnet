using GroceryStore.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Models
{
    public class CustomerCollectionViewModel : ViewModelCollection<Customer>
    {
        private CustomerCollectionViewModel(string selfUrl, IEnumerable<ViewModel<Customer>> data)
            : base(selfUrl, data)
        {
        }

        internal static CustomerCollectionViewModel From(HttpRequest request, IEnumerable<Customer> customers)
        {
            var requestPath = request.Path;

            var data = customers.Select(x => CustomerViewModel.From(request, x));

            var vm = new CustomerCollectionViewModel(requestPath, data);

            return vm;
        }
    }
}
