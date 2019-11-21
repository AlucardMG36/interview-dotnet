using Exceptionless;
using GenFu;
using GroceryStore.Core.Accessors;
using GroceryStore.Core.Entities;
using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace GroceryStoreAPI.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderAccessor> _mockOrders;

        private OrderController _orderController;

        public OrderControllerTests()
        {
            _mockOrders = new Mock<IOrderAccessor>();

            _orderController = GetOrderController();
        }

        [Fact]
        public void GetOrders_ShouldReturnAllOrders()
        {
            //ARRANGE
            var orders = A.ListOf<Order>();

            _mockOrders.Setup(x => x.GetAll()).Returns(orders);

            //ACT
            var result = _orderController.GetOrders();



            //ASSERT
            var value = (result.Result as OkObjectResult).Value;

            var resolvedViewModel = value as OrderCollectionViewModel;

            var resolvedViewModelIds = resolvedViewModel.Data.Select(x => x.Data.Id).ToList();

            value.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => value.ShouldNotBeNull(),
                () => value.ShouldBeOfType(typeof(OrderCollectionViewModel)),
                () =>
                {
                    foreach (var order in orders)
                    {
                        resolvedViewModelIds.ShouldContain(order.Id);
                    };
                }
                );
        }

        [Fact]
        public void GetOrders_ShouldReturnNoContentWhenNoOrdersExist()
        {
            //ARRANGE
            _mockOrders.Setup(x => x.GetAll()).Returns(new List<Order>());

            //ACT
            var result = _orderController.GetOrders();

            //ASSERT
            var value = (result.Result as NoContentResult);

            value.StatusCode.ShouldBe((int)HttpStatusCode.NoContent);
        }

        [Fact]
        public void GetByOrderId_ShouldReturnASingleOrderWhenGivenAnOrderId()
        {
            //ARRANGE
            var order = A.New<Order>();

            _mockOrders.Setup(x => x.WithId(It.IsAny<int>())).Returns(order);

            //ACT
            var result = _orderController.GetByOrderId(order.Id);


            //ASSERT
            var value = (result.Result as OkObjectResult).Value;

            var resolvedViewModel = value as OrderViewModel;

            var resolvedViewModelId = resolvedViewModel.Data.Id;

            value.ShouldSatisfyAllConditions(
            () => result.ShouldNotBeNull(),
            () => value.ShouldNotBeNull(),
            () => value.ShouldBeOfType(typeof(OrderViewModel)),
            () => resolvedViewModelId.ShouldBe(order.Id)
            );
        }

        [Fact]
        public void GetByOrderId_ShouldReturnNoContentWhenOrderIdDoesNotExist()
        {
            //ARRANGE
            _mockOrders.Setup(x => x.WithId(It.IsAny<int>())).Returns(null as Order);

            //ACT
            var result = _orderController.GetByOrderId(RandomData.GetInt(1,10));

            //ASSERT
            var value = (result.Result as NoContentResult);

            value.StatusCode.ShouldBe((int)HttpStatusCode.NoContent);
        }

        [Fact]
        public void GetbyCustomerId_ShouldReturnAllOrdersForAParticularCustomer()
        {
            //ARRANGE
            var customers = A.ListOf<Customer>(RandomData.GetInt(25,1000));

            var selectedCustomer = customers.Random();

            var selectedCustomerId = selectedCustomer.Id;

            A.Configure<Order>()
                .Fill(x => x.CustomerId).WithRandom(customers.Select(x => x.Id));

           var orders = A.ListOf<Order>(RandomData.GetInt(20,500));

            _mockOrders.Setup(x => x.ByCustomer(It.IsAny<int>())).Returns(orders.Where(x => x.CustomerId == selectedCustomerId));

            //ACT
            var result = _orderController.GetbyCustomerId(selectedCustomerId);

            //ASSERT
            var value = (result.Result as OkObjectResult).Value;

            var resolvedViewModel = value as OrderCollectionViewModel;

            var resolvedViewModelIds = resolvedViewModel.Data.Select(x => x.Data.CustomerId).ToList();

            value.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => value.ShouldNotBeNull(),
                () => value.ShouldBeOfType(typeof(OrderCollectionViewModel)),
                () => resolvedViewModelIds.ShouldAllBe(x => x == selectedCustomerId)
                );
        }

        [Fact]
        public void GetbyCustomerId_ShouldReturnNoContentWhenCustomerHasNoOrders()
        {
            //ARRANGE
            var customers = A.ListOf<Customer>(RandomData.GetInt(1, 1000));

            var selectedCustomer = customers.Random();

            var selectedCustomerId = selectedCustomer.Id;
            
            A.Configure<Order>()
                .Fill(x => x.CustomerId).WithRandom(customers.Where(x => x.Id != selectedCustomerId).Select(x => x.Id));

            //ACT
            var result = _orderController.GetbyCustomerId(selectedCustomerId);


            //ASSERT
            var value = (result.Result as NoContentResult);

            value.StatusCode.ShouldBe((int)HttpStatusCode.NoContent);
        }

        private OrderController GetOrderController()
        {
            var controller = new OrderController(_mockOrders.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            return controller;
        }
    }
}
