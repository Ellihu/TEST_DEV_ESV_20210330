using Datos.UnitOfWork;
using Entidades.EntityModels;
using Servicios;
using System;
using System.Collections.Generic;

namespace Logica.Catalogos
{
    public class PersonasFisicasBL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public PersonasFisicasBL(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;

        }

        public IEnumerable<PersonasFisicasEModel> GetAll()
        {
            return _unitOfWork.Repositories.PersonasFisicasRepository.GetAll(); ;
        }
        public IEnumerable<PersonasFisicasEModel> GetActive()
        {
            return _unitOfWork.Repositories.PersonasFisicasRepository.GetAll(); ;
        }

        public PersonasFisicasEModel GetById(int id)
        {
            return _unitOfWork.Repositories.PersonasFisicasRepository.Get(id);
        }
        public void Create(PersonasFisicasEModel model)
        {
            model.Activo = true;
            model.UsuarioAgrega = _userService.UserLogued().Id;
            model.FechaRegistro = DateTime.Now;

            try
            {
                _unitOfWork.Repositories.PersonasFisicasRepository.Create(model);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public PersonasFisicasEModel Update(PersonasFisicasEModel model)
        {
            model.Activo = true;
            model.FechaActualizacion = DateTime.Now;
            var entity = GetById(model.Id);
            try
            {
                if (entity == null)
                    throw new Exception("No se encontró el usuario " + model.Id);

                _unitOfWork.Repositories.PersonasFisicasRepository.Update(model);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return model;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            try
            {
                if (entity == null)
                    throw new Exception("No se encontró el usuario " + id);

                _unitOfWork.Repositories.PersonasFisicasRepository.Remove(entity);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
