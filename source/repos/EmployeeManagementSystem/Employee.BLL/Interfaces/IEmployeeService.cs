using Employee.Application.DTOs.Requests;
using Employee.Application.DTOs.Response;
using Employee.BLL.BOs;
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
        public Task<EmployeeDto?> Delete(int id);
        public Task<EmployeeDto?> Update(int id, EmployeeDto employee);
    }
}
