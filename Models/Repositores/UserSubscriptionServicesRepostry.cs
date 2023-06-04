
using Microsoft.EntityFrameworkCore;
using SportsClub.Core.Pagination.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public class UserSubscriptionServicesRepostry : RepositoryBase<UserSubscriptionService>
    {
        public UserSubscriptionServicesRepostry(SportsClubContext repositoryContext)
            : base(repositoryContext)
        {
        }





        public override IQueryable<UserSubscriptionService> FindByCondition(Expression<Func<UserSubscriptionService, bool>> expression)
        {
            return RepositoryContext.Set<UserSubscriptionService>()
                .Where(expression)
     
                .Include(s => s.Service)
                .Include(s => s.Service.ServiceType)
                .Include(s => s.Service.ServiceTime)
                .Include(s => s.UserSubscription).AsNoTracking();
        }

        public override UserSubscriptionService Find(Expression<Func<UserSubscriptionService, bool>> expression)
        {
            return RepositoryContext.Set<UserSubscriptionService>()
                .Where(expression)
                     .Include(s => s.Service)
                .Include(s => s.Service.ServiceType)
                .Include(s => s.Service.ServiceTime)
                .Include(s => s.UserSubscription)
                .FirstOrDefault();


        }


    }
}
