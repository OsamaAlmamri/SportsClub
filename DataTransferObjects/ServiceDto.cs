using SportsClub.Models;
using System;

namespace SportsClub.DataTransferObjects
{
    public class ServiceDto
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }

        public long ServiceTypeId { get; set; }
        public long? ServiceTimeId { get; set; }

        public string Description { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTimeName { get; set; }
        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
        public double Price { get; set; }

        public DateTime CreatedAt { get; set; }

     


    }
}
