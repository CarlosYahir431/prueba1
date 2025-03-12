using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;
using VelazquezYahir.Services.Services;

namespace VelazquezYahir.Controllers
{
    public class ComentarioController : Controller
    {
        private readonly IBookService _BookService;
        private readonly IComentariosService _ComentarioService;
        public ComentarioController(IComentariosService Comentarioservice, IBookService bookservice)
        {
            _ComentarioService = Comentarioservice;
            _BookService = bookservice;

        }
        [HttpGet]
        public IActionResult Index()
        {
            var ComentarioList = _ComentarioService.GetComentarios();
            return View(ComentarioList);
        }
        public IActionResult Crear()
        {
            ViewBag.Books = _BookService.GetBooks();
            return View();
        }
        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            var result = _ComentarioService.DeleteComentario(id);
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