using Employee.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Interfaces
{
    public interface IEmployeeRepository
    {
        public  Task<List<Employees>> GetAllAsync();
        public  Task<Employees?> GetByIdAsync(int id);
        public Task<Employees>CreateAsync(Employees employee);
        public Task<Employees?> Delete(int id);
        public Task<Employees?> Update(int id , Employees employee);
    }
}
