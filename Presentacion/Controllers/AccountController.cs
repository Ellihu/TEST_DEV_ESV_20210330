using Entidades.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentacion.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AutenticarVModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            AutenticarRespuestaVModel response = _userService.Autenticar(model);

            if (response == null)
            {
                TempData["LoginError"] = "Intento de inicio de sesión no válido.";
                ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                return View(model);
            }

            await AddCookiesAsync(response);
            TempData["JWToken"] = response.Token;

            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");

        }

        async Task AddCookiesAsync(AutenticarRespuestaVModel response)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, response.Email),
                new Claim(ClaimTypes.Email, response.Email),
            };

            var grandmaIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
            await HttpContext.SignInAsync(userPrincipal);
        }
    }
}