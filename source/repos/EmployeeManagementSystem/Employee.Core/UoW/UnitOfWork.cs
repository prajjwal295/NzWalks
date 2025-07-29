using Employee.Dal.Context;
using Employee.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public IDepartmentRepository Departments { get; private set; }

        public IEmployeeRepository Employees { get; private set; }


        public UnitOfWork(EmployeeDbContext employeeDbContext , IDepartmentRepository departmentRepository , IEmployeeRepository employeeRepository) {
            _employeeDbContext = employeeDbContext;
            Departments = departmentRepository;
            Employees = employeeRepository;
        }

        // centralised Saving Operation for the whole system
        public async Task<int> SaveChangesAsync()
        {
            return await _employeeDbContext.SaveChangesAsync();
        }
    }
}
