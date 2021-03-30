using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Repository.Actions
{
    public interface IRemoveRepository<T>
    {
        void Remove(T t);
    }
}
