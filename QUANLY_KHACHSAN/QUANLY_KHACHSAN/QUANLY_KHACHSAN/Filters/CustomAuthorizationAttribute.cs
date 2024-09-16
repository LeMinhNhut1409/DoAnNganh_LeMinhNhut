using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static QUANLY_KHACHSAN.Models.AuthorizationModel;

namespace QUANLY_KHACHSAN.Filters
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {


        private readonly UserRole _requiredRole;

        public CustomAuthorizationAttribute(UserRole requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Assume roles are stored as claims in the user's identity
            var userRoles = user.FindAll("Role").Select(c => c.Value).ToList();

            if (!userRoles.Contains(_requiredRole.ToString("G")))
            {
                context.Result = new ForbidResult();
                return;
            }
        }

    }
}