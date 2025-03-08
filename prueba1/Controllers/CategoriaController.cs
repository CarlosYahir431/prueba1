using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaservice)
        {
            _categoriaService = categoriaservice;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var CategoriaList = _categoriaService.GetCategorias();
            return View(CategoriaList);
        }
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Categoria categoria)
        {
            _categoriaService.CreateCategoria(categoria);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var categoria = _categoriaService.GetCategoriaById(id);
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Editar(Categoria categoria)
        {
            _categoriaService.UpdateCategoria(categoria);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            var result = _categoriaService.DeleteCategoria(id);
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