using Employee.Application.DTOs.Requests;
using Employee.Application.DTOs.Response;
using Employee.BLL.BOs;
using Employee.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<EmployeeDto> CreateAsync(EmployeeDto employee)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto?> Update(int id, EmployeeDto employee)
        {
            throw new NotImplementedException();
        }
    }
}
