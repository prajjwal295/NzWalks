using Employee.Dal.Entities;
using Employee.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Department> Departments { get; }
        IEmployeeRepository Employees { get; }
        IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
    }
}

// This interface defines the contract for a Unit of Work pattern,
//which encapsulates the repositories and provides a method to save changes to the database.