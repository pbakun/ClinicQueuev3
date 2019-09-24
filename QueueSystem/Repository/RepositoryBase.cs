using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public List<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().ToList();
        }

        public void Add(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await this.RepositoryContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }


        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public void UpdateList(IEnumerable<T> entity)
        {
            this.RepositoryContext.Set<T>().UpdateRange(entity);
        }
    }
}
