using Azure.Core;
using Employee.BLL.BOs.Requests;
using Employee.BLL.BOs.Response;
using Employee.BLL.Interfaces;
using Employee.Core.Enums;
using Employee.Core.Utilities;
using Employee.Dal.Entities;
using Employee.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        public AuthService(IUnitOfWork unitOfWork , IJwtTokenRepository jwtTokenRepository)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenRepository = jwtTokenRepository;
        }

        public async  Task<SignupResopnseBO> CreateUserAsync(SignupRequestBO request)
        {
            if (!await UserExists(request.Email, request.Username))
            {
                string[] Roles = ["Admin", "User", "Super User"];

                foreach(var role in request.Roles)
                {
                    if (!Roles.Contains(role))
                    {
                        throw new ArgumentException($"Role '{role}' is not valid.");
                    }
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var user = new User
                {
                    Email = request.Email,
                    Password = hashedPassword,
                    Username = request.Username,
                    Roles = request.Roles,
                };

                 _unitOfWork.Users.Insert(user);

                await _unitOfWork.SaveChangesAsync();

                return new SignupResopnseBO 
                { 
                    Message = "User created successfully.",
                    Succeeded = true,
                    Id = user.id, 
                    Email = user.Email, 
                    Username = user.Username, 
                    Roles = user.Roles 
                };

            }
            else
            {
                return new SignupResopnseBO { Message = "User already exists." , Succeeded = false, };
            }


        }

        public async Task<LoginResponseBO> LoginAsync(LoginRequestBO request)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return new LoginResponseBO
                {
                    Message = "Invalid email or password.",
                    Succeeded = false
                };
            }

            await _unitOfWork.Users.Login(user);
            await _unitOfWork.SaveChangesAsync();

            var token = _jwtTokenRepository.GenerateToken(user.Username , user.Roles , user.id , user.Email);

            if(token == null)
            {
                return new LoginResponseBO
                {
                    Message = "Login Failed",
                    Succeeded = false,
                    Token = ""
                };
            }
            else
            {
                return new LoginResponseBO
                {
                    Message = "Login Successfull",
                    Succeeded = true,
                    Token = token
                };
            }
            
        }


        private async Task<bool>  UserExists(string email, string username)
        {
            var isUserExist = await _unitOfWork.Users.isUserExist(email , username);
            return isUserExist;
        }
    }
}
