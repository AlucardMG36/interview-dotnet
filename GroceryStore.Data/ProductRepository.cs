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
    internal sealed class ProductRepository : IProductAccessor, IEnumerable<Product>
    {
        private readonly RootObject _rootObject;

        public ProductRepository()
        {
            _rootObject = JsonFileReader.RootObject;
        }

        public void Add(Product entity)
        {
            if (_rootObject.Products.Any(x => x.Equals(entity)))
            {
                throw new ArgumentException($"{nameof(entity)}: already exists");
            }

            JsonDatabaseFileWriter.WriteDataToDatabase(entity);
        }

        public IEnumerable<Product> GetAll()
        {
            var products = _rootObject.Products.OrderBy(x => x.Id).ToList();

            return products;
        }

        public IEnumerator<Product> GetEnumerator()
        {
            var products = GetAll();

            return products.GetEnumerator();
        }

        public Product WithId(int entityId)
        {
            var product = GetAll().Where(x => x.Id == entityId).FirstOrDefault();

            return product;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
