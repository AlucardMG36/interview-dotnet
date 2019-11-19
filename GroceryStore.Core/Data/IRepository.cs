using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Core.Data
{
    public interface IRepository<T>
    {
        void Add(T entity);

        //void Delete();

    }
}
