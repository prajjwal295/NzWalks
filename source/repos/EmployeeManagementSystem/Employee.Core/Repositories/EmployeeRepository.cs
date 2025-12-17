using Employee.Dal.Context;
using Employee.Dal.Entities;
using Employee.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Employee.Dal.Repositories
{
    public class EmployeeRepository : GenericRepository<Employees> , IEmployeeRepository
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeRepository(EmployeeDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> IsEmailInUse(string email)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);

            return employee != null;
        }
    }
}
