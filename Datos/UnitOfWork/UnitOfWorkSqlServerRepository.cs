using Datos.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.UnitOfWork
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {
        public IPersonasFisicasRepository PersonasFisicasRepository { get; }
        public IUsuariosRepository UsuariosRepository { get; }


        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            PersonasFisicasRepository = new PersonasFisicasRepository(context, transaction);
            UsuariosRepository = new UsuariosRepository(context, transaction);
            
             
        }

    }
}
