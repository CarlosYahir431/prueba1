    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace VelazquezYahir.Models.Domain
    {
        public class Usuario
        {
            [Key]
            public int PkUsuario { get; set;}
            public required string Nombre { get; set; }
            public required string UserName { get; set; }
            public required string Password { get; set; }
            [ForeignKey ("Roles")]
            public int FkRole { get; set; }
            public Role Roles { get; set; }
        }
    }
