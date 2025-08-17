using Employee.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employees>
    {
        public Task<bool> IsEmailInUse(string email);
    }
}
