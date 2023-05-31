using SportsClub.Models;
using System;

namespace SportsClub.DataTransferObjects
{
    public class UserDetailDto
    {

        public long Id { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }

    }
}
