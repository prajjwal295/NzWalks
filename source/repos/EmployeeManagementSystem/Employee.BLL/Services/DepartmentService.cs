using AutoMapper;
using Employee.BLL.DTOs;
using Employee.BLL.Interfaces;
using Employee.Dal.Entities;
using Employee.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentService(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> CreateAsync(DepartmentDto department)
        {
            var departmentEntity = _mapper.Map<Department>(department);

            departmentEntity.CreatedDate = DateTime.Now;
            departmentEntity.ModifiedDate = DateTime.Now;

            _unitOfWork.Departments.Insert(departmentEntity);
            await _unitOfWork.SaveChangesAsync();

            var departmentBo = _mapper.Map<DepartmentDto>(departmentEntity);
            return departmentBo;
        }


        public async Task<DepartmentDto?> DeleteAsync(int id)
        {
            var existingDepartment = await _unitOfWork.Departments.GetById(id);

            if (existingDepartment == null)
            {
                return null;
            }

             _unitOfWork.Departments.Delete(existingDepartment);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map < DepartmentDto > (existingDepartment);

        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departmentModel = await _unitOfWork.Departments.GetAll();

            var departmentBoList = _mapper.Map<List<DepartmentDto>>(departmentModel);

            return departmentBoList;

        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var departmentModel = await _unitOfWork.Departments.GetById(id);

            if(departmentModel == null)
            {
                return null;
            }
            var departmentBo = _mapper.Map<DepartmentDto>(departmentModel);
            return departmentBo;
        }

        public async Task<DepartmentDto?> UpdateAsync(int id, DepartmentDto department)
        {
            var ExistingDepartment = await _unitOfWork.Departments.GetById(id);

            if (ExistingDepartment == null)
            {
                return null;
            }

            ExistingDepartment.Name = department.Name;
            ExistingDepartment.Description = department.Description;
            ExistingDepartment.ModifiedBy = department.ModifiedBy;
            ExistingDepartment.ModifiedDate = DateTime.Now;

            _unitOfWork.Departments.Update(ExistingDepartment);
            
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(ExistingDepartment);

        }
    }
}
