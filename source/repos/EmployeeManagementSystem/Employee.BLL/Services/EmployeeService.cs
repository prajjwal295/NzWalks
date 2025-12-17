using AutoMapper;
using Employee.BLL.DTOs;
using Employee.BLL.Interfaces;
using Employee.Dal.Entities;
using Employee.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeDto employee)
        {
            if (employee.DepartmentId != null)
            {
                var checkDepartment = await _unitOfWork.Departments.GetById(employee.DepartmentId.Value);
                if (checkDepartment == null)
                {
                    throw new ArgumentException("Invalid DepartmentId. Department does not exist.");
                }
            }

            if(await _unitOfWork.Employees.IsEmailInUse(employee.Email))
            {
                throw new Exception("Email Id is Already in use");
            }

            employee.CreatedDate = DateTime.Now;

            var employeeModel = _mapper.Map<Employees>(employee);

             _unitOfWork.Employees.Insert(employeeModel);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(employeeModel);
        }

        public async Task<EmployeeDto?> DeleteAsync(int id)
        {
            var existingEmployee = await _unitOfWork.Employees.GetById(id);
            if (existingEmployee == null)
            {
                return null;
            }

             _unitOfWork.Employees.Delete(existingEmployee);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(existingEmployee);
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employeesModel = await _unitOfWork.Employees.GetAll(query => query.Include(e => e.Department));
            return _mapper.Map<List<EmployeeDto>>(employeesModel);
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employeeModel = await _unitOfWork.Employees.GetById(id, query => query.Include(e => e.Department));
            if (employeeModel == null)
                return null;

            return _mapper.Map<EmployeeDto>(employeeModel);
        }

        public async Task<EmployeeDto?> UpdateAsync(int id, EmployeeDto employee)
        {
            var existingEmployee = await _unitOfWork.Employees.GetById(id);
            if (existingEmployee == null)
                return null;

            // Validate DepartmentId if it's being updated
            if (employee.DepartmentId.HasValue)
            {
                var checkDepartment = await _unitOfWork.Departments.GetById(employee.DepartmentId.Value);
                if (checkDepartment == null)
                    throw new ArgumentException("Invalid DepartmentId. Department does not exist.");

                existingEmployee.DepartmentId = employee.DepartmentId;
            }

            // Validate email if it's provided and different
            if (!string.IsNullOrWhiteSpace(employee.Email) &&
                !string.Equals(employee.Email, existingEmployee.Email, StringComparison.OrdinalIgnoreCase))
            {
                if (await _unitOfWork.Employees.IsEmailInUse(employee.Email))
                    throw new Exception("Email Id is already in use.");

                existingEmployee.Email = employee.Email;
            }

            // Update fields only if not null
            if (!string.IsNullOrWhiteSpace(employee.FirstName))
                existingEmployee.FirstName = employee.FirstName;

            if (!string.IsNullOrWhiteSpace(employee.LastName))
                existingEmployee.LastName = employee.LastName;

            if (!string.IsNullOrWhiteSpace(employee.PhoneNumber))
                existingEmployee.PhoneNumber = employee.PhoneNumber;

            if (employee.HireDate != default(DateTime))
                existingEmployee.HireDate = employee.HireDate;

            existingEmployee.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Employees.Update(existingEmployee);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(existingEmployee);
        }

    }
}
