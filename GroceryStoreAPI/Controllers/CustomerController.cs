using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStore.Core.Accessors;
using GroceryStore.Core.Entities;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAccessor _customers;

        public CustomerController(ICustomerAccessor customers)
        :base()
        {
            _customers = customers ?? throw new ArgumentException(nameof(customers));
        }


        // GET: api/Customer
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(CustomerCollectionViewModel))]
        [ProducesResponseType(204)]
        public ActionResult<ViewModelCollection<Customer>> Get()
        {
            var customers = _customers.GetAll();

            if(!customers.Any())
            {
                return NoContent();
            }

            var vm = CustomerCollectionViewModel.From(Request, customers);

            return Ok(vm);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public ActionResult<ViewModel<Customer>> Get([FromQuery]int id)
        {
            var customer = _customers.WithId(id);

            if(customer is null)
            {
                return NoContent();
            }

            var vm = CustomerViewModel.From(Request, customer);

            return Ok(vm);
        }

        // POST: api/Customer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
