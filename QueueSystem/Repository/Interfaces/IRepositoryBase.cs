using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        List<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void UpdateList(IEnumerable<T> entity);
        void Delete(T entity);
    }
}
