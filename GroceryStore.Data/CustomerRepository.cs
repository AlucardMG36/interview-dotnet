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
    internal sealed class CustomerRepository : ICustomerAccessor, IEnumerable<Customer>
    {
        private readonly RootObject _rootObject;

        public CustomerRepository()
        {
            _rootObject = JsonFileReader.RootObject;
        }

        public void Add(Customer entity)
        {
            if (_rootObject.Customers.Any(x => x.Equals(entity)))
            {
                throw new ArgumentException($"{nameof(entity)}: already exists");
            }

            JsonDatabaseFileWriter.WriteDataToDatabase(entity);
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = _rootObject.Customers.OrderBy(x => x.Id);

            return customers;
        }

        public IEnumerator<Customer> GetEnumerator()
        {
            var customers = GetAll();

            return customers.GetEnumerator();
        }

        public Customer WithId(int entityId)
        {
            var customer = GetAll().Where(x => x.Id == entityId).FirstOrDefault();

            return customer;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
