using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VelazquezYahir.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["AuthToken"];

            if (!string.IsNullOrEmpty(token))
            {
                AttachUserToContext(context, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"));

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true, // Asegurar que el token tiene fecha de expiración
                    ValidateLifetime = true, // Validar la vida del token
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                // Obtener claims del token
                var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value
                                  ?? jwtToken.Claims.FirstOrDefault(x => x.Type == "sub")?.Value; // Fallback al claim `sub`

                var roleClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "role")?.Value;

                if (!string.IsNullOrEmpty(userIdClaim))
                {
                    context.Items["UserId"] = userIdClaim;
                }

                if (!string.IsNullOrEmpty(roleClaim))
                {
                    context.Items["Role"] = roleClaim;
                }

                // Establecer el usuario autenticado en HttpContext.User
                var claimsIdentity = new ClaimsIdentity(principal.Claims, "jwt");
                context.User = new ClaimsPrincipal(claimsIdentity);
            }
            catch (SecurityTokenExpiredException)
            {
                // Si el token expiró, lo eliminamos de las cookies para evitar problemas
                context.Response.Cookies.Delete("AuthToken");
            }
            catch
            {
                // Si hay algún otro error en la validación del token, el middleware sigue sin usuario autenticado
            }
        }
    }

    // Extensión para registrar el middleware
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
