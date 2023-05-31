using SportsClub.Models;
using System;

namespace SportsClub.DataTransferObjects
{
    public class ServiceTimeDto
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

    }
}
