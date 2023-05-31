using SportsClub.Core.Pagination.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public interface IRepositoryBase<T>
    {
        //      IList<T> List();

        //     IList<T> Search(string term);
        public Task<List<T>> GetAllPage(PaginationFilter filter, Expression<Func<T,long>> Orderexpression);
        IQueryable<T> FindAll(); 
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression); 
        T Find(Expression<Func<T, bool>> expression); 
        void Create(T entity); 
        T LastInserted(Expression<Func<T, dynamic>> expression); 
        void Update(T entity); 
        void Delete(T entity);
    }
}
