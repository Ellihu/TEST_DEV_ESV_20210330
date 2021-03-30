using Entidades.EntityModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Datos.Repository
{
    public class PersonasFisicasRepository : IPersonasFisicasRepository
    {
        protected SqlConnection _context;
        protected SqlTransaction _transaction;
        public PersonasFisicasRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public void Create(PersonasFisicasEModel entity)
        {
            using (SqlCommand cmd = new SqlCommand("sp_AgregarPersonaFisica", _context, _transaction))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Nombre", entity.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", entity.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", entity.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@RFC", entity.RFC));
                cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", entity.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@UsuarioAgrega", entity.UsuarioAgrega));

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    var response = new
                    {
                        Error = Convert.ToInt32(reader["ERROR"]),
                        ErrorMessage = Convert.ToString(reader["MENSAJEERROR"]),
                    };

                    if (response.Error < 1)
                        throw new Exception(response.ErrorMessage);
                };
            }
        }

        public PersonasFisicasEModel Get(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tb_PersonasFisicas  WHERE IdPersonaFisica = @id", _context, _transaction))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    return new PersonasFisicasEModel
                    {
                        Id = Convert.ToInt32(reader["IdPersonaFisica"]),
                        FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                        FechaActualizacion = ObjectToNullableDateTime(reader["FechaActualizacion"]),
                        Nombre = Convert.ToString(reader["Nombre"]),
                        ApellidoPaterno = Convert.ToString(reader["ApellidoPaterno"]),
                        ApellidoMaterno = Convert.ToString(reader["ApellidoMaterno"]),
                        RFC = Convert.ToString(reader["RFC"]),
                        FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        UsuarioAgrega = Convert.ToInt32(reader["UsuarioAgrega"]),
                        Activo = Convert.ToBoolean(reader["Activo"]),
                    };
                };
            }
        }

        public IEnumerable<PersonasFisicasEModel> GetAll()
        {
            var result = new List<PersonasFisicasEModel>();
            
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tb_PersonasFisicas Where Activo=1", _context, _transaction))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new PersonasFisicasEModel
                            {
                                Id = Convert.ToInt32(reader["IdPersonaFisica"]),
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                                FechaActualizacion = ObjectToNullableDateTime(reader["FechaActualizacion"]),
                                Nombre = Convert.ToString(reader["Nombre"]),
                                ApellidoPaterno = Convert.ToString(reader["ApellidoPaterno"]),
                                ApellidoMaterno = Convert.ToString(reader["ApellidoMaterno"]),
                                RFC = Convert.ToString(reader["RFC"]),
                                FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                                UsuarioAgrega = Convert.ToInt32(reader["UsuarioAgrega"]),
                                Activo = Convert.ToBoolean(reader["Activo"]),
                            }); ;
                        }
                    }
                }
           
            return result;
        }


        public void Update(PersonasFisicasEModel entity)
        {

            using (SqlCommand cmd = new SqlCommand("sp_ActualizarPersonaFisica", _context, _transaction))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdPersonaFisica", entity.Id));
                cmd.Parameters.Add(new SqlParameter("@Nombre", entity.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", entity.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", entity.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@RFC", entity.RFC));
                cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", entity.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@UsuarioAgrega", entity.UsuarioAgrega));

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    var response = new
                    {
                        Error = Convert.ToInt32(reader["ERROR"]),
                        ErrorMessage = Convert.ToString(reader["MENSAJEERROR"]),
                    };

                    if (response.Error < 1)
                        throw new Exception(response.ErrorMessage);
                };
            }

        }
        public void Remove(PersonasFisicasEModel entity)
        {

            using (SqlCommand cmd = new SqlCommand("sp_EliminarPersonaFisica", _context, _transaction))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdPersonaFisica", entity.Id));

                var ss = cmd.ExecuteNonQuery();
            }

        }
        DateTime? ObjectToNullableDateTime(object? value)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
                return Convert.ToDateTime(value);
            return null;
        }

    }
}
