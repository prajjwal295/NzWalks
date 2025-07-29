using Employee.Dal.Entities;
using Employee.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<Employees> CreateAsync(Employees employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employees?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employees>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employees?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Employees?> Update(int id, Employees employee)
        {
            throw new NotImplementedException();
        }
    }
}
