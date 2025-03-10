using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Controllers
{
    public class AuthvController : Controller
    {
        private readonly IUserService _userService;

        public AuthvController(IUserService userService)
        {
            _userService = userService;
        }

        // Método para mostrar la vista de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Método para manejar la solicitud de login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.GetUsers().FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                // Autenticación exitosa, redirigir según el rol del usuario
                if (user.Roles.Nombre == "Admin") // Asumiendo que el rol tiene un nombre
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (user.Roles.Nombre == "User")
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // Autenticación fallida, mostrar mensaje de error
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
                return View();
            }
        }

        // Método para mostrar la vista de registro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Método para manejar la solicitud de registro
        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (_userService.CreateUser(usuario))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al registrar el usuario.");
                }
            }
            return View(usuario);
        }
    }
}
