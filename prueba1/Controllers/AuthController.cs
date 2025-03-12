using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace VelazquezYahir.Controllers
{
    public class AuthController : Controller
    {
        private readonly VelazquezYahir.Services.IServices.IAuthenticationService _authService;

        public AuthController(VelazquezYahir.Services.IServices.IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = _authService.Authenticate(model.Username, model.Password);

            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
                return View(model);
            }

            var user = _authService.GetUserByUsername(model.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Error al obtener la información del usuario.");
                return View(model);
            }

            // **Almacenar el token JWT en una cookie segura**
            HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Solo en HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            // **Autenticar en el sistema de ASP.NET Core**
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.PkUsuario.ToString()),
                new Claim(ClaimTypes.Role, user.FkRole.ToString()) // Se recomienda usar Enum para roles
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity), authProperties);

            // **Redirección basada en rol**
            return user.FkRole switch
            {
                1 => RedirectToAction("Index", "User"),
                2 => RedirectToAction("Index", "UsuarioVerdadero"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // **Hashear la contraseña antes de guardarla**
            var hashedPassword = _authService.HashPassword(model.Password);

            var usuario = new Usuario
            {
                UserName = model.Username,
                Password = hashedPassword, // Guardar el password hasheado
                FkRole = model.Role,
                Nombre = model.Nombre
            };

            if (_authService.RegisterUser(usuario))
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "Error al registrar el usuario.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // **Eliminar la cookie del token**
            HttpContext.Response.Cookies.Delete("AuthToken");

            // **Cerrar sesión**
            await HttpContext.SignOutAsync("Cookies");

            return RedirectToAction("Login");
        }
    }
}
