using VelazquezYahir.Models.Domain;

namespace VelazquezYahir.Models.ViewModels
{
    public class BookDetailViewModel
    {
        public Book Book { get; set; }
        public List<ComentarioViewModel> Comentarios { get; set; }
        public string NuevoComentario { get; set; }
        public int UserId { get; set; } // Agregado para enviar el ID del usuario autenticado
    }

    public class ComentarioViewModel
    {
        public string UsuarioNombre { get; set; }  // Nombre del usuario
        public string ComentarioTexto { get; set; }  // Texto del comentario
    }
}
