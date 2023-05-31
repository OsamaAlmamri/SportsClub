
using Microsoft.EntityFrameworkCore;
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
