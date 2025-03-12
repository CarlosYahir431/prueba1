using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoriaService _categoriaService;

        public BookController(IBookService bookservice, ICategoriaService categoriaService)
        {
            _bookService = bookservice;

            _categoriaService = categoriaService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var BookList = _bookService.GetBooks();
            return View(BookList);
        }
        public IActionResult Crear()
        {
            ViewBag.Categorias = _categoriaService.GetCategorias();
            return View();
        }
        [HttpPost]
        public IActionResult Crear(Book libro)
        {
            _bookService.CreateBook(libro);
            return RedirectToAction("Index");
        }
        public IActionResult Editar(int id)
        {
            ViewBag.Categorias = _categoriaService.GetCategorias();
            var libro = _bookService.GetBookById(id);
            return View(libro);
        }

        [HttpPost]
        public IActionResult Editar(Book libro)
        {
            _bookService.UpdateBook(libro);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            var result = _bookService.DeleteBook(id);
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