
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public class UserDetailRepostry : RepositoryBase<UserDetail>
    { 
        public UserDetailRepostry(SportsClubContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

 
      
    }
}
