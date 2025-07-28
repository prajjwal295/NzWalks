using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Model.DTO;
using NzWalks.API.Repositories;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //UserManager Class is helpfull in order to create the User
        // It comes from the Microsoft.AspNetCore.Identity
        // We have injected this package in the program.cs

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepositroy _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepositroy tokenRepositroy )
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepositroy;
        }
        //POST : /api/Auth/Register

        [HttpPost]
        [Route("Register")]
        public async  Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add Roles to the User
                if(registerRequestDto.Roles!=null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please Login.");
                    }
                }
            }

            return BadRequest("User Registration Failed!!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

            if(user!=null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if(checkPasswordResult)
                {
                    //GetRoles

                   var roles =  await _userManager.GetRolesAsync(user);

                    if(roles!=null)
                    {
                        //Create Token
                        var jwtToken =  _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(jwtToken);
                    }
                }
            }

            return BadRequest("User Login Failed!!");
        }
    }
}
