using SportsClub.Models;
using System;

namespace SportsClub.DataTransferObjects
{
    public class UserSubscriptionServiceDto
    {

        public long Id { get; set; }
        public long ServiceId { get; set; }
        public long UserSubscriptionId { get; set; }
        public string UserId { get; set; }

        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public  ServiceDto Service { get; set; }
       

    }
}
