using System.ComponentModel.DataAnnotations;

namespace VelazquezYahir.Models.Domain
{
    public class Categoria
    {
        [Key]
        public int PkCategoria { get; set; }
        public string Nombre { get; set; }
    }
}
