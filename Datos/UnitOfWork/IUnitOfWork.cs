using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWorkRepository Repositories { get; }
        void SaveChanges();
    }
}
