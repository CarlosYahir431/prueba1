using VelazquezYahir.Models.Domain;

namespace VelazquezYahir.Services.IServices
{
    public interface IUserService
    {
        public List<Usuario> GetUsers();
        public bool CreateUser(Usuario request);
        public Usuario GetUserById(int id);
        public bool UpdateUser(Usuario request);
        public bool DeleteUser(int PkUsuario);
    }
}
