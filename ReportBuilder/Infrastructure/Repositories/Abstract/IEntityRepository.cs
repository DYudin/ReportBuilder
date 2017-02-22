using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReportBuilder.Infrastructure.Repositories.Abstract 
{
    public interface IEntityRepository<T> where T : class, new()
    {
        IEnumerable<T> FindByIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAll();

        //T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> predicate);

        T GetSingleWithRelated(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Commit();
    }
}
