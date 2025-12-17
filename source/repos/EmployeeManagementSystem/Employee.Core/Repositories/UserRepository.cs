using Employee.Dal.Context;
using Employee.Dal.Entities;
using Employee.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Repositories
{
    public class UserRepository : GenericRepository<User> , IUserRepository
    {
        private readonly EmployeeDbContext _context;

        public UserRepository(EmployeeDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Login(User user)
        {
            user.LastLogin = DateTime.Now;
            return;
        }

        public async Task<bool> isUserExist(string email, string username)
        {
            return await _context.Users.AnyAsync(u => u.Email == email || u.Username == username);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
           return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
