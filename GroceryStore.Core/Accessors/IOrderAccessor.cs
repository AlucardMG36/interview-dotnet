using GroceryStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Core.Accessors
{
    public interface IOrderAccessor: IAccessor<Order>
    {
        IEnumerable<Order> ByDate(DateTime searchDate);

        IEnumerable<Order> ByCustomer(int customerId);
    }
}
