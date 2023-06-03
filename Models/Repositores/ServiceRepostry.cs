
using Microsoft.EntityFrameworkCore;
using SportsClub.Core.Pagination.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public class ServiceRepostry : RepositoryBase<Service>
    { 
        public ServiceRepostry(SportsClubContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }


        public override IQueryable<Service> FindAll()
        {
            //    return RepositoryContext.Set<T>().AsNoTracking();
            return  RepositoryContext.Set<Service>()
            .Include(s => s.ServiceTime)
            .Include(s => s.ServiceType)
            .OrderByDescending(s => s.Id)
            .AsNoTracking();
        }

        public override  async Task<List<Service>> GetAllPage(PaginationFilter filter, Expression<Func<Service , dynamic>> Orderexpression)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await RepositoryContext.Set<Service>()
            .Include(s=>s.ServiceTime)
            .Include(s => s.ServiceType)
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();           
        }

        public override Service Find(Expression<Func<Service, bool>> expression)
        {
            return RepositoryContext.Set<Service>()
                .Where(expression)
                .Include(s => s.ServiceTime)
                .Include(s => s.ServiceType)
                .FirstOrDefault();

        
        }
        public override Service LastInserted(Expression<Func<Service, dynamic>> expression)
        {
       
            return RepositoryContext.Set<Service>()
                  .Include(s => s.ServiceTime)
                .Include(s => s.ServiceType)
                .OrderByDescending(expression).First();
               

        
        }


    }
}
