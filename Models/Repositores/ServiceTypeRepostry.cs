
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

 
      
    }
}
