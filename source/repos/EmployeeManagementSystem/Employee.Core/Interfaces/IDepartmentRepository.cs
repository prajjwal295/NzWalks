using Employee.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Interfaces
{
    public interface IDepartmentRepository
    {
        public Task<List<Department>> GetAllAsync();
        public Task<Department?> GetByIdAsync(int id);
        public Task<Department> CreateAsync(Department department);
        public Task<Department?> Delete(int id);
        public Task<Department?> Update(int id, Department department);
    }
}
