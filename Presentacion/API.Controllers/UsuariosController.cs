using Entidades.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace Presentacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsuariosController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Autenticar")]
        public IActionResult Autenticar([FromBody] AutenticarVModel model)
        {
            AutenticarRespuestaVModel response = _userService.Autenticar(model);

            if (response == null)
                return new ForbidResult();


            return Ok(new { Data = response.Token });
        }
    }
}