using Exceptionless;
using GenFu;
using GroceryStore.Core.Helpers;
using Shouldly;
using System;
using Xunit;

namespace GroceryStore.Core.Tests
{
    public class JsonHelperTests
    {
        [Fact]
        public void ToClass_ShouldDesiralizeToAppropriateType()
        {
            //ARRANGE
            var json = $"{A.New<int>()}";

            //ACT
            var result = JsonHelper.ToClass<int>(json);

            //ASSERT
            result.ShouldBeOfType<int>();
        }

        [Fact]
        public void FromClass_ShouldSerializeToJsonFromClass()
        {
            //ARRANGE
            var testValue = new { StartDate = A.New<DateTime>(), EndDate = A.New<DateTime>() };

            //ACT
            var result = JsonHelper.FromClass(testValue);

            //ASSERT
            result.ShouldBeOfType<string>();
        }
    }
}
