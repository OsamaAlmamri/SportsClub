
using Microsoft.EntityFrameworkCore;
using SportsClub.Core.Pagination.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public class UserSubscriptionRepostry : RepositoryBase<UserSubscription>
    {
        public UserSubscriptionRepostry(SportsClubContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public override async Task<List<UserSubscription>> GetAllPage(PaginationFilter filter, Expression<Func<UserSubscription, dynamic>> Orderexpression)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await RepositoryContext.Set<UserSubscription>()
            .Include(s => s.User)
            .Include(s => s.UserSubscriptionServices)
            .Include(s => s.UserServicePaymentGatways)
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
        }

        public override UserSubscription Find(Expression<Func<UserSubscription, bool>> expression)
        {
            return RepositoryContext.Set<UserSubscription>()
                .Where(expression)
                .Include(s => s.User)
                      .Include(s => s.UserSubscriptionServices)
                .Include(s => s.UserServicePaymentGatways)
                .FirstOrDefault();


        }
        public override UserSubscription LastInserted(Expression<Func<UserSubscription, dynamic>> expression)
        {

            return RepositoryContext.Set<UserSubscription>()
                 .Include(s => s.User)
            .Include(s => s.UserServicePaymentGatways)
                  .Include(s => s.UserSubscriptionServices)
                .OrderByDescending(expression).First();



        }


    }
}
