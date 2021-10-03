using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProductCategories.DataAccess
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        int Count();

        int Count(Expression<Func<T, bool>> predicate);

        T Get(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        //bool Update(Expression<Func<T, bool>> predicate, T entity);
        void Update(Expression<Func<T, bool>> predicate, T entity);

        void Delete(T entity, bool forceDelete = false);

        void Delete(Expression<Func<T, bool>> predicate, bool forceDelete = false);

        void Delete<TField>(FieldDefinition<T, TField> field, TField date);
        void DeleteEq<TField>(FieldDefinition<T, TField> field, TField eq);

        bool Any(Expression<Func<T, bool>> predicate);
    }
}
