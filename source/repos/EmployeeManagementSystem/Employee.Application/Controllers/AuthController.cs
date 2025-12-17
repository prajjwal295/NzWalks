using Employee.Application.Validators;
using Employee.BLL.BOs.Requests;
using Employee.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Employee.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("/Register")]
        [ValidateModel]
        public async Task<IActionResult> Signup([FromBody] SignupRequestBO request)
        {
            try
            {
                _logger.LogInformation("Signup request received for email: {Email}", request.Email);

                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Username))
                {
                    _logger.LogWarning("Signup failed: Invalid input.");
                    return BadRequest("Invalid signup request.");
                }

                var signupResult = await _authService.CreateUserAsync(request);

                if (signupResult.Succeeded)
                {
                    _logger.LogInformation("Signup succeeded for email: {Email}", request.Email);
                    return Ok(signupResult);
                }

                _logger.LogWarning("Signup failed for email: {Email}", request.Email);
                return BadRequest(signupResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during signup for email: {Email}", request.Email);
                return StatusCode(500, $"An error occurred while signing up: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("/Login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginRequestBO request)
        {
            try
            {
                _logger.LogInformation("Login request received for email: {Email}", request.Email);

                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    _logger.LogWarning("Login failed: Invalid input.");
                    return BadRequest("Invalid login request.");
                }

                var loginResult = await _authService.LoginAsync(request);

                if (loginResult.Succeeded)
                {
                    _logger.LogInformation("Login succeeded for email: {Email}", request.Email);
                    return Ok(loginResult);
                }

                _logger.LogWarning("Login failed for email: {Email}", request.Email);
                return Unauthorized(loginResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", request.Email);
                return StatusCode(500, $"An error occurred while logging in: {ex.Message}");
            }
        }
    }
}
