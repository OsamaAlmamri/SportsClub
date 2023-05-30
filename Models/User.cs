using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace SportsClub.Models
{

    public partial class User : IdentityUser<String>
    {
        // public String Id { get; set; }

        public User()
        {
            UserSubscriptionServices = new HashSet<UserSubscriptionService>();
            UserSubscriptions = new HashSet<UserSubscription>();
        }
     
        //public string? UserName { get; set; }
        //public string? Email { get; set; }

        public String UserType { get; set; }
      
        public DateTime CreatedAt { get; set; }

        public virtual UserDetail? UserDetail { get; set; }

        public virtual ICollection<UserSubscriptionService> UserSubscriptionServices { get; set; }
        public virtual ICollection<UserSubscription> UserSubscriptions { get; set; }

    }
}
