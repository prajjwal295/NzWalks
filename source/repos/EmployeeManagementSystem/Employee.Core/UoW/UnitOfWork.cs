using Employee.Dal.Context;
using Employee.Dal.Entities;
using Employee.Dal.Interfaces;
using Employee.Dal.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmployeeDbContext _employeeDbContext;

    private IEmployeeRepository employeeRepository;
    private GenericRepository<Department> departmentRepository;
    private IUserRepository userRepository;

    public UnitOfWork(EmployeeDbContext employeeDbContext)
    {
        _employeeDbContext = employeeDbContext;
    }

    public IEmployeeRepository Employees
    {
        get
        {
            if (employeeRepository == null)
                employeeRepository = new EmployeeRepository(_employeeDbContext);

            return employeeRepository;
        }
    }

    public IGenericRepository<Department> Departments
    {
        get
        {
            if (departmentRepository == null)
                departmentRepository = new GenericRepository<Department>(_employeeDbContext);

            return departmentRepository;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (userRepository == null)
                userRepository = new UserRepository(_employeeDbContext);

            return userRepository;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _employeeDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _employeeDbContext.Dispose();
    }
}
