using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NzWalks.API.CustomActionFilter
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) 
        {
            Roles = string.Join(",", roles);
        }
    } 
}
