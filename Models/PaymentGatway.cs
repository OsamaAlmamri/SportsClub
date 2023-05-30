using System;
using System.Collections.Generic;

#nullable disable

namespace SportsClub.Models
{
    public partial class PaymentGatway
    {

        public PaymentGatway()
        {
            UserServicePaymentGatways = new HashSet<UserSubscriptionPaymentGatway>();
        }
        public long Id { get; set; }
   
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? DeployKey { get; set; }
        public string? TestKey { get; set; }
        public string? SecretKey { get; set; }
        public string? PublicKey { get; set; }
        public string? Environment { get; set; }
        public string? Secret { get; set; }


        //"paypal_client_id":"0000","paypal_secret":"0000"

        public virtual ICollection<UserSubscriptionPaymentGatway> UserServicePaymentGatways { get; set; }







    }
}
