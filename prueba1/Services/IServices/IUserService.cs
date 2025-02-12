using VelazquezYahir.Models.Domain;

namespace VelazquezYahir.Services.IServices
{
    public interface IUserService
    {
        public List<Usuario> GetUsers();
        public bool CreateUser(Usuario request);

    }
}
