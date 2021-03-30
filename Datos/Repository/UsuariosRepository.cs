using Entidades.EntityModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
        protected SqlConnection _context;
        protected SqlTransaction _transaction;
        public UsuariosRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;

        }


        public UsuariosEModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuariosEModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public UsuariosEModel GetByEmail(string email)
        {
            return GetAllUser().FirstOrDefault(x=> x.Email== email);
        }


        List<UsuariosEModel> GetAllUser()
        {
            var list = new List<UsuariosEModel>();

            list.Add(new UsuariosEModel
            {
                Id = 1,
                Nombre = "Test",
                ApellidoPaterno = "Test",
                ApellidoMaterno = "Test",
                Email = "test@gmail.com",
                Contraseina = "123456",
                Activo = true
            });
            list.Add(new UsuariosEModel
            {
                Id = 1,
                Nombre = "Test2",
                ApellidoPaterno = "Test",
                ApellidoMaterno = "Test",
                Email = "test2@gmail.com",
                Contraseina = "123456",
                Activo = true
            });
            return list;
        }

    }
}
