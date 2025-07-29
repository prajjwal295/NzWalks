using Employee.Application.DTOs.Requests;
using Employee.Application.DTOs.Response;
using Employee.BLL.BOs;
using Employee.BLL.Interfaces;
using Employee.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        public Task<DepartmentDto> CreateAsync(DepartmentDto department)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDto?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DepartmentDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDto?> Update(int id, DepartmentDto department)
        {
            throw new NotImplementedException();
        }
    }
}
