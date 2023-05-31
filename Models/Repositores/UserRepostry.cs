
using Microsoft.EntityFrameworkCore;
using SportsClub.Core.Pagination.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public class UserRepostry : RepositoryBase<User>
    { 
        public UserRepostry(SportsClubContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

        public override  async Task<List<User>> GetAllPage(PaginationFilter filter, Expression<Func<User, dynamic>> Orderexpression)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await RepositoryContext.Set<User>()
            
            .Include(s => s.UserDetail)
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();           
        }

        public override User Find(Expression<Func<User, bool>> expression)
        {
            return RepositoryContext.Set<User>()
                .Where(expression)
                .Include(s => s.UserDetail)
                .FirstOrDefault();

        
        }
        public override User LastInserted(Expression<Func<User, dynamic>> expression)
        {
       
            return RepositoryContext.Set<User>()
                  .Include(s => s.UserDetail)
                .OrderByDescending(expression).First();
               

        
        }


    }
}
