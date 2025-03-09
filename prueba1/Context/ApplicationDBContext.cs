using Microsoft.EntityFrameworkCore;
using VelazquezYahir.Models.Domain;

namespace VelazquezYahir.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    PkUsuario = 1,
                    Nombre = "Carlos",
                    UserName = "Arrozquemado49",
                    Password = "caritademiel123xd",
                    FkRole = 1
                }
                );

            modelBuilder.Entity<Role>().HasData(
            new Role
            {
                PkRole = 1,
                Nombre = "Admin"
            },
                   new Role
                   {
                       PkRole = 2,
                       Nombre = "Usuario"
                   }
            );
        }
    }
}
