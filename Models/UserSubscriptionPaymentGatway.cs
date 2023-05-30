using System;
using System.Collections.Generic;

#nullable disable

namespace SportsClub.Models
{
    public partial class UserSubscriptionPaymentGatway
    {
       

        public long Id { get; set; }

        public long UserSubscriptionId { get; set; }
        public long PaymentGatwayId { get; set; }


        public string ResponseCode { get; set; }
        public string TransactionId { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
       

        public virtual UserSubscription UserSubscription { get; set; }
        public virtual PaymentGatway PaymentGatway { get; set; }




    }
}
