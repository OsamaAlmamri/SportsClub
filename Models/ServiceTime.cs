using System;
using System.Collections.Generic;

#nullable disable

namespace SportsClub.Models
{
    public partial class ServiceTime
    {
        public ServiceTime()
        {
            Services = new HashSet<Service>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public virtual ICollection<Service> Services { get; set; }



    }
}
