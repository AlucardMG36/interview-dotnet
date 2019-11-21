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
    public class ProductControllerTests
    {
        private readonly Mock<IProductAccessor> _mockProducts;

        private ProductController _productController;

        public ProductControllerTests()
        {
            _mockProducts = new Mock<IProductAccessor>();

            _productController = GetProductController();
        }

        [Fact]
        public void GetProducts_ShouldReturnAllProductsInDatabase()
        {
            //ARRANGE
            var products = A.ListOf<Product>(RandomData.GetInt(1, 1000));

            _mockProducts.Setup(x => x.GetAll()).Returns(products);

            //ACT
            var result = _productController.GetProducts();

            //ASSERT
            var value = (result.Result as OkObjectResult).Value;

            var resolvedViewModel = value as ProductCollectionViewModel;

            var resolvedViewModelIds = resolvedViewModel.Data.Select(x => x.Data.Id).ToList();

            value.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => value.ShouldNotBeNull(),
                () => value.ShouldBeOfType(typeof(ProductCollectionViewModel)),
                () =>
                {
                    foreach (var product in products)
                    {
                        resolvedViewModelIds.ShouldContain(product.Id);
                    };
                }
                );
        }

        [Fact]
        public void GetProducts_ShouldReturnNoContentWhenNoOrdersExist()
        {
            //ARRANGE
            _mockProducts.Setup(x => x.GetAll()).Returns(new List<Product>());

            //ACT
            var result = _productController.GetProducts();

            //ASSERT
            var value = (result.Result as NoContentResult);

            value.StatusCode.ShouldBe((int)HttpStatusCode.NoContent);
        }

        [Fact]
        public void GetByProductId_ShouldReturnASingleOrderWhenGivenAnOrderId()
        {
            //ARRANGE
            var product = A.New<Product>();

            _mockProducts.Setup(x => x.WithId(It.IsAny<int>())).Returns(product);

            //ACT
            var result = _productController.GetProductbyId(product.Id);


            //ASSERT
            var value = (result.Result as OkObjectResult).Value;

            var resolvedViewModel = value as ProductViewModel;

            var resolvedViewModelId = resolvedViewModel.Data.Id;

            value.ShouldSatisfyAllConditions(
            () => result.ShouldNotBeNull(),
            () => value.ShouldNotBeNull(),
            () => value.ShouldBeOfType(typeof(ProductViewModel)),
            () => resolvedViewModelId.ShouldBe(product.Id)
            );
        }

        [Fact]
        public void GetByProduct_ShouldReturnNoContentWhenOrderIdDoesNotExist()
        {
            //ARRANGE
            _mockProducts.Setup(x => x.WithId(It.IsAny<int>())).Returns(null as Product);

            //ACT
            var result = _productController.GetProductbyId(RandomData.GetInt(1, 10));

            //ASSERT
            var value = (result.Result as NoContentResult);

            value.StatusCode.ShouldBe((int)HttpStatusCode.NoContent);
        }

        private ProductController GetProductController()
        {
            var controller = new ProductController(_mockProducts.Object)
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
