using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VelazquezYahir.Models.Domain;
using VelazquezYahir.Services.IServices;

namespace VelazquezYahir.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService; 
            _configuration = configuration;
        }

        public bool RegisterUser(Usuario usuario)
        {
            // Hash de la contraseña antes de guardarla
            usuario.Password = HashPassword(usuario.Password);
            return _userService.CreateUserverdadero(usuario);
        }

        public string? Authenticate(string username, string password)
        {
            var user = _userService.GetUsers().FirstOrDefault(u => u.UserName == username);

            if (user == null)
                return null;

            // Verificar contraseña hasheada
            if (!VerifyPassword(password, user.Password))
                return null;

            // Generar token JWT
            return GenerateJwtToken(user);
        }

        public Usuario? GetUserByUsername(string username)
        {
            return _userService.GetUsers().FirstOrDefault(u => u.UserName == username);
        }

        private string GenerateJwtToken(Usuario user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.PkUsuario.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            // Usando PBKDF2 para hashear la contraseña
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                // Verificar que el hash no sea nulo o vacío
                if (string.IsNullOrEmpty(storedHash))
                    return false;

                byte[] hashBytes = Convert.FromBase64String(storedHash);

                // Verificar que el hash tenga la longitud correcta
                if (hashBytes.Length != 36)
                    return false;

                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
                byte[] hash = pbkdf2.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                        return false;
                }
                return true;
            }
            catch (FormatException)
            {
                // Log error si es necesario
                return false;
            }
            catch (Exception)
            {
                // Log error si es necesario
                return false;
            }
        }
    }
}
