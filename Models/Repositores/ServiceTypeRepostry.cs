
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public class ServiceTypeRepostry : RepositoryBase<ServiceType>
    { 
        public ServiceTypeRepostry(SportsClubContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

 
        public void Create(ServiceType serviceType)
        {
            Create(serviceType);
        }

        public void Update(ServiceType serviceType)
        {
            Update(serviceType);
        }

        public void Delete(ServiceType serviceType)
        {
            Delete(serviceType);
        }
     
        public override ServiceType LastInserted()
        {
            return RepositoryContext.ServiceTypes.OrderByDescending(a => a.Id).First();

        }
    }
}
