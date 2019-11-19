using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
    public sealed class Link
    {
        public Link(string name, string href)
        {
            SetHref(href);
            SetName(name);
        }

        public string Href { get; private set; }

        public string Name { get; private set; }

        public override string ToString()
        {
            return $"{Name}: {Href}";
        }

        private void SetHref(string href)
        {
            Href = href ?? throw new ArgumentNullException(nameof(href));
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }
}
