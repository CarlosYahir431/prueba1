using VelazquezYahir.Models.Domain;

namespace VelazquezYahir.Services.IServices
{
    public interface IAuthenticationService
    {
        bool RegisterUser(Usuario usuario);
        string? Authenticate(string username, string password);
        Usuario? GetUserByUsername(string username);
    }
}