using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PruebaExamen.Models;
using PruebaExamen.Repositories;
using System.Security.Claims;

namespace PruebaExamen.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryUsuarios repo;

        public ManagedController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email
    , string password)
        {
            Usuario user = await this.repo.GetUserByEmailPasswordAsync(email, password);
            if (user != null)
            {
                ClaimsIdentity identity =
               new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme
               , ClaimTypes.Name, ClaimTypes.Role);

                Claim claimName = new Claim("nombre", user.Nombre);
                identity.AddClaim(claimName);

                Claim claimId = new Claim("id", user.IdUsuario.ToString());
                identity.AddClaim(claimId);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();

                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Peliculas");
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
