using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Models.ViewModels;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Controllers
{
    public class UsuarioVerdaderoController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoriaService _categoriaService;
        private readonly IUserService _userService;
        private readonly IComentariosService _comentarioService;
        // GET: UsuarioVerdaderoController
        public UsuarioVerdaderoController(IBookService bookservice, ICategoriaService categoriaservice, IUserService userservice, IComentariosService comentarioservice)
        {
            _bookService = bookservice;
            _categoriaService = categoriaservice;
            _userService = userservice;
            _comentarioService = comentarioservice;
        }
        public ActionResult Index()
        {
            var BookList = _bookService.GetBooks();
            return View(BookList);
        }


        public ActionResult Details(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var comentarios = _comentarioService.GetComentarios()
                .Where(c => c.PkBook == id)
                .Select(c => new ComentarioViewModel
                {
                    UsuarioNombre = _userService.GetUserById(c.PkUsuario)?.Nombre ?? "Anónimo",
                    ComentarioTexto = c.Comentarios
                })
                .ToList();

            var viewModel = new BookDetailViewModel
            {
                Book = book,
                Comentarios = comentarios
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarComentario(BookDetailViewModel model, int UserId)
        {
            if (ModelState.IsValid)
            {
                var usuario = _userService.GetUserById(UserId); // Obtener usuario autenticado
                if (usuario == null)
                {
                    ModelState.AddModelError("", "No se encontró el usuario.");
                    return RedirectToAction("Details", new { id = model.Book.PkBook });
                }

                Comentario nuevoComentario = new Comentario
                {
                    PkBook = model.Book.PkBook,
                    Comentarios = model.NuevoComentario,
                    PkUsuario = usuario.PkUsuario
                };

                bool resultado = _comentarioService.CreateComentario(nuevoComentario);

                if (!resultado)
                {
                    ModelState.AddModelError("", "Error al guardar el comentario.");
                }
            }

            return RedirectToAction("Details", new { id = model.Book.PkBook });
        }


        // GET: UsuarioVerdaderoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioVerdaderoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioVerdaderoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioVerdaderoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioVerdaderoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioVerdaderoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
