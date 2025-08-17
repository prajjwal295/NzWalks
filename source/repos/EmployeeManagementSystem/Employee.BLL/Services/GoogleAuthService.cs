using Employee.BLL.BOs.Requests;
using Employee.BLL.Interfaces;
using Employee.Dal.Context;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Employee.BLL.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly ILogger<GoogleAuthService> _logger;
        private readonly IConfiguration _config;

        public GoogleAuthService(
            EmployeeDbContext dbContext,
            ILogger<GoogleAuthService> logger,
            IConfiguration config)
        {
            _dbContext = dbContext;
            _logger = logger;
            _config = config;
        }

        public async Task<GoogleJsonWebSignature.Payload?> GoogleSignIn(GoogleSignInVM model)
        {
            try
            {
                var clientId = _config["Google:client_id"];

                var payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                });

                return payload;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Google Sign-In failed for token: {IdToken}", model.IdToken);
                return null;
            }
        }
    }
}
