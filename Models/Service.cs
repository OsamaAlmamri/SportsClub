using System;
using System.Collections.Generic;

#nullable disable

namespace SportsClub.Models
{
    public partial class Service
    {

        public Service()
        {
            UserServices = new HashSet<UserSubscriptionService>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }

        public long ServiceTypeId { get; set; }
        public long? ServiceTimeId { get; set; }
     
        public string Description { get; set; }
        public double Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ServiceType ServiceType { get; set; }
        public virtual ServiceTime ServiceTime { get; set; }

        public virtual ICollection<UserSubscriptionService> UserServices { get; set; }



    }
}
