using Employee.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Employee.Application.Middleware
{
    public class CustomAuthMiddleware  : IMiddleware
    {
        private readonly IJwtTokenRepository _jwtTokenRepository;
        public CustomAuthMiddleware(IJwtTokenRepository jwtTokenRepository)
        {
            this._jwtTokenRepository = jwtTokenRepository;
        }

        public async Task InvokeAsync(HttpContext context , RequestDelegate next)
        {
            if (context.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                await next(context);
                return;
            }

            var authorizeData = context.GetEndpoint()?.Metadata?.GetMetadata<IAuthorizeData>();

            if (authorizeData != null)
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();

                if (token == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new { message = "Token is missing." });
                    return;
                }

                // Authentication Logic
                var claimsPrincipal = _jwtTokenRepository.ValidateToken(token);
                if (claimsPrincipal == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new { message = "Invalid token." });
                    return;
                }

                context.User = claimsPrincipal;


                //Authorizsation 

                //fetch the Roles fr the authorizeData
                var requiredRoles = authorizeData.Roles?.Split(',').Select(r => r.Trim()).ToList();

                if(requiredRoles!=null && requiredRoles.Any())
                {
                    var userRoles = claimsPrincipal.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList();
                    if (!userRoles.Any(role => requiredRoles.Contains(role)))
                    {
                        context.Response.StatusCode = 403; 
                        await context.Response.WriteAsJsonAsync(new { message = "You do not have permission to access this resource." });
                        return;
                    }
                }
            }
            await next(context);
            return;
        }
    }
}
