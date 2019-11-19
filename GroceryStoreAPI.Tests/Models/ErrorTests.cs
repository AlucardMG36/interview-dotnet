using GroceryStoreAPI.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GroceryStoreAPI.Tests.Models
{
    public class ErrorTests
    {
        [Fact]
        public void Constructor_ShouldThrowAnArgumentNullExceptionWhenTheMessageArgumentIsAnEmptyString()
        {
            //ARRANGE
            Action error = () => new Error(String.Empty);

            //ACT & ASSERT
            error.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ShouldThrowAnArgumentNullExceptionWhenTheMessageArgumentIsNull()
        {
            //ARRANGE
            Action error = () => new Error(null);

            //ACT & ASSERT
            error.ShouldThrow<ArgumentNullException>();
        }
    }
}
