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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace GroceryStoreAPI.Tests.Controllers
{
    public class CustomerControllerTests
    {
        private Mock<ICustomerAccessor> _mockCustomers;

        private CustomerController _customerController;

        public CustomerControllerTests()
        {
            _mockCustomers = new Mock<ICustomerAccessor>();

            _customerController = CreateCustomerController();
        }

        [Fact]
        public void GetAll_ShouldReturnACustomerCollectionViewModel()
        {
            //ARRANGE
            var customers = A.ListOf<Customer>(RandomData.GetInt(1, 1000));

            _mockCustomers.Setup(x => x.GetAll()).Returns(customers);

            //ACT
            var result = _customerController.GetAll();

            //ASSERT
            var value = (result.Result as OkObjectResult).Value;

            var resolvedViewModel = value as CustomerCollectionViewModel;

            var resolvedViewModelIds = resolvedViewModel.Data.Select(x => x.Data.Id).ToList();

            value.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => value.ShouldNotBeNull(),
                () => value.ShouldBeOfType(typeof(CustomerCollectionViewModel)),
                () =>
                {
                    foreach (var customer in customers)
                    {
                        resolvedViewModelIds.ShouldContain(customer.Id);
                    };
                }
                );
        }

        [Fact]
        public void GetAll_ShouldReturnNoContentWhenNoCustomersAreRetrieved()
        {
            //ARRANGE
            _mockCustomers.Setup(x => x.GetAll()).Returns(new List<Customer>());

            //ACT
            var result = _customerController.GetAll();

            //ASSERT
            var value = (result.Result as NoContentResult);

            value.StatusCode.ShouldBe((int)HttpStatusCode.NoContent);
        }


        [Fact]
        public void GetById_ShouldReturnASingleCustomerViewModelWhenGivenAnId()
        {
            //ARRANGE
            var customers = A.ListOf<Customer>(RandomData.GetInt(1, 1000));

            var customerToReturn = customers.Random();

            _mockCustomers.Setup(x => x.WithId(It.IsAny<int>())).Returns(customerToReturn);

            //ACT
            var result = _customerController.GetById(customerToReturn.Id);

            //ASSERT
            var value = (result.Result as OkObjectResult).Value;

            var resolvedViewModel = value as CustomerViewModel;

            var resolvedViewModelId = resolvedViewModel.Data.Id;

            value.ShouldSatisfyAllConditions(
            () => result.ShouldNotBeNull(),
            () => value.ShouldNotBeNull(),
            () => value.ShouldBeOfType(typeof(CustomerViewModel)),
            () => resolvedViewModelId.ShouldBe(customerToReturn.Id)
            );
        }

        [Fact]
        public void GetById_ShouldReturnNoContentWhenCustomerIsNotFound()
        {
            //ARRANGE
            var customers = A.ListOf<Customer>(RandomData.GetInt(1, 1000));

            var customerToReturn = customers.Random();

            _mockCustomers.Setup(x => x.WithId(It.IsAny<int>())).Returns(null as Customer);

            //ACT
            var result = _customerController.GetById(customerToReturn.Id);

            //ASSERT
            var value = (result.Result as NoContentResult);

            value.StatusCode.ShouldBe((int)HttpStatusCode.NoContent);
        }


        public CustomerController CreateCustomerController()
        {
            var controller = new CustomerController(_mockCustomers.Object)
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
