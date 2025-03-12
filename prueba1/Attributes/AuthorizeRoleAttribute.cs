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
            var userRole = context.HttpContext.Items["Role"];

            if (userId == null)
            {
                // No autenticado, redirigir a login
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            // Verificar si el rol del usuario está entre los roles permitidos
            if (_roles.Length > 0 && !_roles.Contains(userRole?.ToString()))
            {
                // No autorizado para acceder a este recurso
                context.Result = new ForbidResult();
            }
        }
    }
}