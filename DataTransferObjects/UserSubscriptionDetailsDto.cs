using SportsClub.Models;
using System;

namespace SportsClub.DataTransferObjects
{
    public class UserSubscriptionDetailsDto
    {

        public long Id { get; set; }
        public string UserId { get; set; }


        public bool ISPayed { get; set; }

        public double TotaAmount { get; set; }

        public string Currency { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserSubscriptionServiceDto Services { get; set; }
        public UserSubscriptionPaymentGatway payments { get; set; }
        public UserDto User { get; set; }


    }
}
