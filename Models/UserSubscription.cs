using System;
using System.Collections.Generic;

#nullable disable

namespace SportsClub.Models
{
    public partial class UserSubscription
    {

        public UserSubscription()
        {
            UserServicePaymentGatways = new HashSet<UserSubscriptionPaymentGatway>();
            UserSubscriptionServices = new HashSet<UserSubscriptionService>();
        }
        public long Id { get; set; }
   
        public string UserId { get; set; }
     
  
        public bool ISPayed { get; set; }

        public double TotaAmount { get; set; }

        public string Currency { get; set; }

        public DateTime CreatedAt { get; set; }
    
        public virtual User User { get; set; }

       
        public virtual ICollection<UserSubscriptionPaymentGatway> UserServicePaymentGatways { get; set; }

        public virtual ICollection<UserSubscriptionService> UserSubscriptionServices { get; set; }





    }
}
