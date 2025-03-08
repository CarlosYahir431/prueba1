using Microsoft.EntityFrameworkCore;
using VelazquezYahir.Context;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;
        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }
        public List<Usuario> GetUsers() { return _context.Usuarios.Include(x => x.Roles).ToList(); }

        public bool CreateUser(Usuario request)
        {
            try
            {
                Usuario usuario = new()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRole = request.FkRole
                };
                _context.Usuarios.Add(usuario);
                int result = _context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {

                throw new Exception("sucedio un error" + ex.Message);
            }

        }
    }
}
