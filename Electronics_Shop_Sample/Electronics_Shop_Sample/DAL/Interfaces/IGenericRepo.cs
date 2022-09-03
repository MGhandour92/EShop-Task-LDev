using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Main
{
    public interface IGenericRepo<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(int id, T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
