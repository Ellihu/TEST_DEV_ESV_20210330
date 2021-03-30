using Datos.UnitOfWork;
using Entidades.EntityModels;
using Logica.Catalogos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System;
using System.Collections.Generic;

namespace Presentacion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonasFisicasController : ControllerBase
    {
        private readonly PersonasFisicasBL _personasFisicasBL;
        private readonly IUserService _userService;

        public PersonasFisicasController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _userService = userService;
            _personasFisicasBL = new PersonasFisicasBL(unitOfWork, userService);
        }


        [HttpGet]
        public IEnumerable<PersonasFisicasEModel> Get()
        {
            var user = User.Identity.Name;
            return _personasFisicasBL.GetActive();
        }


        [HttpGet("{id}", Name = "Get")]
        public PersonasFisicasEModel Get(int id)
        {
            return _personasFisicasBL.GetById(id);
        }


        [HttpPost]
        public IActionResult Create([FromBody] PersonasFisicasEModel model)
        {
            try
            {
                _personasFisicasBL.Create(model);

                return Ok(new { Data = true });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        public IActionResult Update([FromBody] PersonasFisicasEModel model)
        {
            try
            {
                return Ok(_personasFisicasBL.Update(model));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _personasFisicasBL.Delete(id);
                return Ok(new { Data = true });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
