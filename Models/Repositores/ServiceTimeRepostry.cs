﻿
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public class ServiceTimeRepostry : RepositoryBase<ServiceTime>
    { 
        public ServiceTimeRepostry(SportsClubContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

 
        public void Create(ServiceTime serviceTime)
        {
            Create(serviceTime);
        }

        public void Update(ServiceTime serviceTime)
        {
            Update(serviceTime);
        }

        public void Delete(ServiceTime serviceTime)
        {
            Delete(serviceTime);
        }
     
        public override ServiceTime LastInserted()
        {
            return RepositoryContext.ServiceTimes.OrderByDescending(a => a.Id).First();

        }
    }
}
