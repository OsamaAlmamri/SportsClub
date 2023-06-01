using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsClub.Core.Requests
{
    public class UserSubscriptionRequest
    {
        [Required]
        public List<UserSubscriptionServicesRequest> Services { get; set; }




    }

    public class UserSubscriptionServicesRequest
    {
        [Required]
        public long ServiceId { get; set; }
 
        public DateTime? StartAt { get; set; }
    }
}
