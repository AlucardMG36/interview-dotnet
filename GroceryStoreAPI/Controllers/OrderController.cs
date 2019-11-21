using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStore.Core.Accessors;
using GroceryStore.Core.Entities;
using GroceryStore.Core.Helpers;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderAccessor _orders;

        public OrderController(IOrderAccessor orders)
        {
            _orders = orders ?? throw new ArgumentException(nameof(orders));
        }

        // GET: api/Order
        [HttpGet]
        public ActionResult<ViewModelCollection<Order>> GetOrders()
        {
            var orders = _orders.GetAll();

            if (!orders.Any())
            {
                return NoContent();
            }

            var vm = OrderCollectionViewModel.From(Request, orders);

            return Ok(vm);
        }

        // GET: api/Order/5
        [HttpGet("GetByOrderId")]
        public ActionResult<ViewModel<Order>> GetByOrderId([FromQuery]int id)
        {
            var order = _orders.WithId(id);

            if (order is null)
            {
                return NoContent();
            }

            var vm = OrderViewModel.From(Request, order);

            return Ok(vm);

        }

        [HttpGet("GetByCustomerId")]
        public ActionResult<ViewModelCollection<Order>> GetbyCustomerId([FromQuery]int customerId)
        {
            var customerOrders = _orders.ByCustomer(customerId);

            if(!customerOrders.Any())
            {
                return NoContent();
            }

            var vm = OrderCollectionViewModel.From(Request, customerOrders);
            
            return Ok(vm);
        }

        // POST: api/Order
        [HttpPost("AddOrder")]
        [ProducesResponseType(201)]
        public IActionResult AddOrder([FromBody] Order order)
        {
            _orders.Add(order);

            return Created(Request.Path.Value, order);
        }
    }
}
