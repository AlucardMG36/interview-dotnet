using Exceptionless;
using GroceryStoreAPI.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GroceryStoreAPI.Tests.Models
{
    public class LinkTests
    {
        [Fact]
        public void Constructor_ShouldThrowAnArgumentNullExceptionWhenTheHrefArgumentIsNull()
        {
            //ARRANGE
            var name = RandomData.GetAlphaString(1, 10);

            //ACT
            Action link = () => new Link(name, null);

            //ASSERT
            link.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ShouldThrowAnArgumentNullExceptionWhenTheNameArgumentIsAnEmptyString()
        {
            //ARRANGE
            var href = "/" + RandomData.GetAlphaString(1, 10);

            //ACT
            Action link = () => new Link(String.Empty, href);

            //ASSERT
            link.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ShouldThrowAnArgumentNullExceptionWhenTheNameArgumentIsNull()
        {
            //ARRANGE
            var href = "/" + RandomData.GetAlphaString(1, 10);

            //ACT
            Action link = () => new Link(null, href);

            //ASSERT
            link.ShouldThrow<ArgumentNullException>();

        }
    }
}
