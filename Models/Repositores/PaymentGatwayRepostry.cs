
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsClub.Models.Repositores
{
    public class PaymentGatwayRepostry : RepositoryBase<PaymentGatway>
    { 
        public PaymentGatwayRepostry(SportsClubContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

 
        
    }
}
