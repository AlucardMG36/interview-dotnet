using GroceryStore.Core.Accessors;
using GroceryStore.Core.Entities;
using GroceryStore.Data.Readers;
using GroceryStore.Data.Writers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStore.Data
{
    internal sealed class OrderRepository : IOrderAccessor, IEnumerable<Order>
    {
        private readonly ICustomerAccessor _customers;

        private readonly IProductAccessor _products;

        private readonly RootObject _rootObject;

        public OrderRepository()
        {
            _rootObject = JsonFileReader.RootObject;
        }

        public OrderRepository(ICustomerAccessor customers, IProductAccessor products)
            : this()
        {
            _customers = customers ?? throw new ArgumentException(nameof(customers));

            _products = products ?? throw new ArgumentException(nameof(products));
        }

        public void Add(Order entity)
        {
            if (_rootObject.Orders.Any(x => x.Equals(entity)))
            {
                throw new ArgumentException($"{nameof(entity)}: already exists");
            }

            JsonDatabaseFileWriter.WriteDataToDatabase(entity);
        }

        public IEnumerable<Order> ByCustomer(int customerId)
        {
            var ordersByCustomer = GetAll().Where(x => x.CustomerId == customerId).ToList();

            return ordersByCustomer;
        }

        public IEnumerable<Order> ByDate(DateTime searchDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _rootObject.Orders.OrderBy(x => x.CustomerId);

            foreach (var order in orders)
            {
                LoadCustomerForOrder(order);
                LoadOrderItemsForOrder(order);
            }

            return orders;
        }

        public IEnumerator<Order> GetEnumerator()
        {
            var orders = GetAll();

            return orders.GetEnumerator();
        }

        public Order WithId(int entityId)
        {
            var order = GetAll().Where(x => x.Id == entityId).FirstOrDefault();

            return order;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void LoadCustomerForOrder(Order order)
        {
            order.Customer = _customers.WithId(order.CustomerId);
        }

        private void LoadOrderItemsForOrder(Order order)
        {
            foreach (var orderItem in order.Items)
            {
                orderItem.Product = _products.WithId(orderItem.ProductId);
            }

        }
    }
}
