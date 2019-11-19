using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Core.Accessors
{
    public interface IAccessor<T>
    {
        IEnumerable<T> GetAll();

        T WithId(int entityId);
    }
}
