using Datos.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        IPersonasFisicasRepository PersonasFisicasRepository { get; }
        IUsuariosRepository UsuariosRepository { get; }
    }
}
