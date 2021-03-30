using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Repository.Actions
{
    public interface ICreateRepository<T> where T : class
    {
        void Create(T t);
    }
}
