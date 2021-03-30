using Entidades.EntityModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class PersonasFisicasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Crear()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            PersonasFisicasEModel model = new PersonasFisicasEModel { Id = id };
            var user = User.Identity.Name;

            return View(model);
        }
    }
}