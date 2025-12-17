using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Employee.Core.Enums;

namespace Employee.Core.Utilities
{
    public class JwtTokenGenerator : IJwtTokenRepository
    {
        private readonly IConfiguration _config;

        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string? GenerateToken(string username, string[] roles, int userId, string email)
        {
            string[] Roles = ["Admin", "User", "Super User"];

            foreach (var role in roles)
            {
                if (!Roles.Contains(role))
                {
                    throw new ArgumentException($"Role '{role}' is not valid.");
                }
            }

            var claims = new[]
            {
                new Claim("UserName", username),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            foreach (var role in roles)
            {
                claims = claims.Append(new Claim(ClaimTypes.Role, role.ToString())).ToArray();
            }
            ;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            if (token != null)
            {
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            return principal;
        }
    }
}
