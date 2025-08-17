using Employee.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Utilities
{
    public interface IJwtTokenRepository
    {
        string? GenerateToken(string username, string[] roles, int userId, string email);
        ClaimsPrincipal ValidateToken(string token);
    }
}
