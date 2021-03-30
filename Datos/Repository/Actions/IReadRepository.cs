using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Repository.Actions
{
    public interface IReadRepository<T, Y> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Y id);
    }
}
