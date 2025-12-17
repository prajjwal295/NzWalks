using Employee.Dal.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task Login(User user);

        public Task<bool> isUserExist(string email, string username);

        public Task<User?> GetByEmailAsync(string email);
    }
}


// this interface extends the generic repository interface for User entity
// it basically means, we dont need to implement the crud again , here we are only creating some custom methods for user entity