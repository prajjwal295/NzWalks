using Employee.Dal.Entities;
using Employee.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Repositories
{
    public class DepartentRepository : IDepartmentRepository
    {
        public Task<Department> CreateAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> Update(int id, Department department)
        {
            throw new NotImplementedException();
        }
    }
}
