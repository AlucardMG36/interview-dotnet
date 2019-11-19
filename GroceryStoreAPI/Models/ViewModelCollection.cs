using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GroceryStoreAPI.Models
{
    public class ViewModelCollection<T>
        : ViewModel<ICollection<ViewModel<T>>>
    {
        public ViewModelCollection(string selfUrl)
            : base(selfUrl, new Collection<ViewModel<T>>())
        {
        }

        public ViewModelCollection(string selfUrl, IEnumerable<ViewModel<T>> data)
            : base(selfUrl, data?.ToList())
        {
        }
    }
}
