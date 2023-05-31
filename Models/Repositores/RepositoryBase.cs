using Microsoft.EntityFrameworkCore;
using SportsClub.Core.Pagination.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected SportsClubContext RepositoryContext { get; set; } 
        public RepositoryBase(SportsClubContext repositoryContext) 
        {
            RepositoryContext = repositoryContext; 
        } 
        
        public virtual IQueryable<T> FindAll() 
        { 
            return RepositoryContext.Set<T>().AsNoTracking(); 
        } 
        
        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        { 
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }
        


        public virtual void  Create(T entity) 
        { 
            RepositoryContext.Set<T>().Add(entity);
            RepositoryContext.SaveChanges();
         
        } 
        
        public virtual void Update(T entity) 
        {
            RepositoryContext.Set<T>().Update(entity);
            RepositoryContext.SaveChanges();
        } 
        public virtual void pagenation(T entity) 
        {
            RepositoryContext.Set<T>().Update(entity);
        }
        //.OrderByDescending(a => a.Id).
        public virtual  async Task<List<T>> GetAllPage(PaginationFilter filter, Expression<Func<T, long>> Orderexpression) 
       {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await RepositoryContext.Set<T>()
                .OrderByDescending(Orderexpression)
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               
               .ToListAsync();
        }

        public virtual void Delete(T entity)
        { 
            RepositoryContext.Set<T>().Remove(entity);
            RepositoryContext.SaveChanges();
        }

      
        public virtual T LastInserted(Expression<Func<T, dynamic>> expression)
        {
            return RepositoryContext.Set<T>().OrderByDescending(expression).First();
        }

        public virtual T Find(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking().FirstOrDefault();

         
        }

    }
}
