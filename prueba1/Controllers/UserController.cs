using Microsoft.AspNetCore.Mvc;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userservice)
        {
            _userService = userservice;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var GetUser = _userService.GetUsers();
            return View(GetUser);
        }
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _userService.CreateUser(usuario); // Llama al servicio para crear el usuario

                return RedirectToAction("Index");
            }
            return View(usuario);
        }
    }
}