
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

        public async Task<List<Service>> GetAllPage(PaginationFilter filter, Expression<Func<Service , long>> Orderexpression)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await RepositoryContext.Set<Service>()
            .Include(s=>s.ServiceTime)
            .Include(s => s.ServiceType)
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)

               .ToListAsync();
        }

        public void Create(Service service)
        {
            Create(service);
        }

        public void Update(Service service)
        {
            Update(service);
        }

        public void Delete(Service service)
        {
            Delete(service);
        }
     
        public override Service LastInserted()
        {
            return RepositoryContext.Services.OrderByDescending(a => a.Id).First();

        }
    }
}
