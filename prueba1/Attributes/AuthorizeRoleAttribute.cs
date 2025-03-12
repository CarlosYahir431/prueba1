using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VelazquezYahir.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public AuthorizeRoleAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.Items["UserId"];
            var userRole = context.HttpContext.Items["Role"]?.ToString();

            if (userId == null)
            {
                // No autenticado, redirigir a login
                if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
                {
                    context.Result = new UnauthorizedResult(); // 401 para APIs
                }
                else
                {
                    context.Result = new RedirectToActionResult("Login", "Auth", null);
                }
                return;
            }

            // Verificar si el rol del usuario está entre los permitidos (ignorando mayúsculas/minúsculas)
            if (_roles.Length > 0 && !_roles.Any(r => string.Equals(r, userRole, StringComparison.OrdinalIgnoreCase)))
            {
                context.Result = new ForbidResult(); // 403 Forbidden
            }
        }

    }
}