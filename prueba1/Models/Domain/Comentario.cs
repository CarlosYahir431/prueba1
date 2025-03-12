using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VelazquezYahir.Models.Domain
{
    public class Comentario
    {
        [Key]
        public int PkComentario { get; set; }
        public required string Comentarios { get; set; }
        [ForeignKey("Book")]
        public required int PkBook { get; set; }
        public Book Book { get; set; }
        public int PkUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
