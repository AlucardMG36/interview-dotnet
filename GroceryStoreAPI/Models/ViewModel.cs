using GroceryStoreAPI.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Models
{
    public class ViewModel<T>
    {
        public const string SelfLinkName = "self";

        private T _data;

        private List<Error> _errors;

        private List<Link> _links;

        public ViewModel(string selfUrl)
        {
            if (selfUrl is null)
            {
                throw new ArgumentNullException(nameof(selfUrl));
            }

            _links = new List<Link>()
            {
                new Link(SelfLinkName,  selfUrl)
            };

        }

        public ViewModel(string selfUrl, T data)
            : this(selfUrl)
        {
            _data = data;
        }

        public T Data
        {
            get => _data;
            set => SetData(value);
        }

        public IEnumerable<Error> Errors => _errors;

        public IEnumerable<Link> Links => _links;
        
        public Error AddError(string message, int statusCode)
        {
            EnsureDataHasNotBeenSet();

            EnsureErrorsCollectionIsNotNull();

            var error = new Error(message, statusCode);

            _errors.Add(error);

            return error;
        }

        public Link AddLink(string name, string href)
        {
            var link = new Link(name, href);

            _links.Add(link);

            return link;
        }

        public Link GetLink(string name) => Links.SingleOrDefault(c => c.Name.Equals(name));

        private void EnsureDataHasNotBeenSet()
        {
            if (_data != null)
            {
                throw new InvalidOperationException(Resources.ViewModelCannotAddError);
            }
        }

        private void EnsureErrorsCollectionIsNotNull()
        {
            if (_errors is null)
            {
                _errors = new List<Error>();
            }
        }

        private void EnsureErrorsHasNotBeenSet()
        {
            if (_errors != null)
            {
                throw new InvalidOperationException(Resources.ViewModelCannotSetDataProperty);
            }
        }

        private void SetData(T data)
        {
            EnsureErrorsHasNotBeenSet();

            _data = data;
        }

    }
}
