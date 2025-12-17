using Employee.Dal.Context;
using Employee.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(EmployeeDbContext dbContext)
        {
            this._dbContext = dbContext;

            // whatever class name we specify while creating the instance of Generic Repositor y
            // that class name will be stores in the table Variable
            this._dbSet = _dbContext.Set<T>();
        }


        public async void Delete(T obj)
        {
            _dbSet.Remove(obj);
        }

        public async Task<IEnumerable<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(object id, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<object>(e, "Id") == id);
        }

        public async void Insert(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        //learn this
        public async void Update(T obj)
        {
            _dbSet.Attach(obj);
            _dbSet.Entry(obj).State = EntityState.Modified;
        }
    }
}
