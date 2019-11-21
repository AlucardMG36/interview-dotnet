using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Core.Accessors
{
    public interface IAccessor<T>
    {
        void Add(T entity);

        IEnumerable<T> GetAll();

        T WithId(int entityId);
    }
}
