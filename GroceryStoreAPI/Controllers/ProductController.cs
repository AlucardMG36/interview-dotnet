using GroceryStore.Core.Accessors;
using GroceryStore.Core.Entities;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductAccessor _products;

        public ProductController(IProductAccessor products)
        {
            _products = products ?? throw new ArgumentException(nameof(products));
        }

        // GET: api/Product
        [HttpGet]
        public ActionResult<ViewModelCollection<Product>> GetProducts()
        {
            var products = _products.GetAll();

            if (!products.Any())
            {
                return NoContent();
            }

            var vm = ProductCollectionViewModel.From(Request, products);

            return Ok(vm);
        }

        // GET: api/Product/GetProductById?id=5
        [HttpGet("GetProductById")]
        [ProducesResponseType(200, Type = typeof(ProductViewModel))]
        [ProducesResponseType(204)]
        public ActionResult<ViewModel<Product>> GetProductbyId([FromQuery]int id)
        {
            var product = _products.WithId(id);

            if (product is null)
            {
                return NoContent();
            }

            var vm = ProductViewModel.From(Request, product);

            return Ok(vm);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody] Product value)
        {
            _products.Add(value);

            return Created(Request.Path.Value, value);
        }

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
