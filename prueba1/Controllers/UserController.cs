using Azure.Core;
using Microsoft.AspNetCore.Authorization;
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
            _userService.CreateUser(usuario);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        { 
            var usuario = _userService.GetUserById(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            _userService.UpdateUser(usuario);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            var result =_userService.DeleteUser(id);
            if (result == true)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}