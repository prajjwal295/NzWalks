using Employee.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.BLL.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<DepartmentDto> CreateAsync(DepartmentDto department);
        Task<DepartmentDto?> UpdateAsync(int id, DepartmentDto department);
        Task<DepartmentDto?> DeleteAsync(int id);
    }
}