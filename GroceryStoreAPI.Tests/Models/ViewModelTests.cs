using Exceptionless;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Properties;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GroceryStoreAPI.Tests.Models
{
    public class ViewModelTests
    {
        private readonly string _data;
        private readonly string _href;
        private readonly string _name;
        private readonly ViewModel<String> _sut;

        public ViewModelTests()
        {
            _data = RandomData.GetAlphaString(1, 10);
            _href = $"/{RandomData.GetAlphaString(1, 10)}";
            _name = RandomData.GetAlphaString(1, 10);
            _sut = new ViewModel<string>(_href);
        }

        [Fact]
        public void AddError_ShouldAddEachErrorWhenDataIsNull()
        {
            //ARRANGE
            var count = RandomData.GetInt(2, 10);
            var messages = new string[count];

            //ACT
            for (var i = 0; i < count; i++)
            {
                messages[i] = RandomData.GetAlphaString(1, 10);

                _sut.AddError(messages[i], RandomData.GetInt(1, 10));
            }

            _sut.Errors.ShouldSatisfyAllConditions(
                () => _sut.Errors.Count().ShouldBe(count),
                () => _sut.Errors.Where(x => String.IsNullOrWhiteSpace(x.Message)).ShouldBeEmpty()
                );
        }

        [Fact()]
        public void AddErrorShouldThrowAnInvalidOperationExceptionWhenDataIsNotNull()
        {
            //ARRANGE
            var message = RandomData.GetAlphaString(1, 10);
            _sut.Data = _data;

            //ACT
            Action act = () => _sut.AddError(message, RandomData.GetInt());

            //ASSERT
            act.ShouldThrow<InvalidOperationException>(Resources.ViewModelCannotAddError);
        }

        [Fact()]
        public void AddLinkShouldAddEachLink()
        {
            //ARRANGE
            var second_name = RandomData.GetAlphaString(1, 10);
            var second_href = "/" + RandomData.GetAlphaString(1, 10);

            //ACT
            _sut.AddLink(_name, _href);
            _sut.AddLink(second_name, second_href);

            //ASSERT
            _sut.Links.Count().ShouldBe(3); //The "self" link is added automatically when the view model is instantiated
            _sut.Links.SingleOrDefault(x => x.Name.Equals(_name) && x.Href.Equals(_href)).ShouldNotBeNull();
            _sut.Links.SingleOrDefault(x => x.Name.Equals(_name) && x.Href.Equals(_href)).ShouldNotBeNull();
        }

        [Fact()]
        public void _dataShouldReturnNullWhenItDoesNotExist()
        {
            //ARRANGE

            //ACT
            var result = _sut.Data;

            //ASSERT
            result.ShouldBeNull();
        }

        [Fact()]
        public void _dataShouldREturnTheObjectWhenItExists()
        {
            //ARRANGE
            _sut.Data = _data;

            //ACT
            var result = _sut.Data;

            //ASSERT
            result.ShouldBe(_data);
        }

        [Fact()]
        public void _dataShouldThrowAnInvalidOperationExceptionWhenSettingItToAnObjectAfterAnErrorWasAdded()
        {
            //ARRANGE
            var message = RandomData.GetAlphaString(1, 10);

            _sut.AddError(message, RandomData.GetInt());

            //ACT
            Action act = () => _sut.Data = _data;

            //ASSERT
            act.ShouldThrow<InvalidOperationException>(Resources.ViewModelCannotSetDataProperty);
        }

        [Fact()]
        public void GetLinkShouldReturnTheCorrectlinkWhenItExists()
        {
            //ARRANGE
            _sut.AddLink(_name, _href);

            //ACT
            var result = _sut.GetLink(_name);

            //ASSERT
            result.Href.ShouldBe(_href);
            result.Name.ShouldBe(_name);
        }

        [Fact()]
        public void GetLinkShouldReturnNullWhenTheLinkDoesNotExist()
        {
            //ARRANGE

            //ACT
            var result = _sut.GetLink(_name);

            //ASSERT
            result.ShouldBeNull();
        }
    }
}
