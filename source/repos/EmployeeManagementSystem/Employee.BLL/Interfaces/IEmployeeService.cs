using Employee.BLL.DTOs;
using Employee.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Interfaces
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeDto>> GetAllAsync();
        public Task<EmployeeDto?> GetByIdAsync(int id);
        public Task<EmployeeDto> CreateAsync(EmployeeDto employee);
        public Task<EmployeeDto?> DeleteAsync(int id);
        public Task<EmployeeDto?> UpdateAsync(int id, EmployeeDto employee);
    }
}
