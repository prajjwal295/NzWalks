using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Employee.Dal.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<T> GetById(object id, Func<IQueryable<T>, IQueryable<T>>? include = null);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}


// This code defines a generic repository interface for CRUD operations on entities of type T.