using Employee.Application.DTOs.Requests;
using Employee.Application.DTOs.Response;
using Employee.BLL.BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Interfaces
{
    public interface IDepartmentService
    {
        public Task<List<DepartmentDto>> GetAllAsync();
        public Task<DepartmentDto?> GetByIdAsync(int id);
        public Task<DepartmentDto> CreateAsync(DepartmentDto department);
        public Task<DepartmentDto?> Delete(int id);
        public Task<DepartmentDto?> Update(int id, DepartmentDto department);
    }
}
