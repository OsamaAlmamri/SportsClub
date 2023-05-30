using System;
using System.Collections.Generic;

#nullable disable

namespace SportsClub.Models
{
    public partial class UserSubscriptionService
    {

        public UserSubscriptionService()
        {
            UserServicePaymentGatways = new HashSet<UserSubscriptionPaymentGatway>();
        }
        public long Id { get; set; }
        public long ServiceId { get; set; }
        public long UserSubscriptionId { get; set; }
        public string UserId { get; set; }

        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
        public virtual UserSubscription UserSubscription { get; set; }


        public virtual ICollection<UserSubscriptionPaymentGatway> UserServicePaymentGatways { get; set; }





    }
}
