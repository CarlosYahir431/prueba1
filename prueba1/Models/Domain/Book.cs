using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VelazquezYahir.Models.Domain
{
    public class Book
    {
        [Key]
        public int PkBook { get; set; }
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required string Autor { get; set; }
        public required string Img { get; set; }
        [ForeignKey("Categorias")]
        public required int Categoria { get; set; }
        public  Categoria Categorias { get; set; }
    }
}
