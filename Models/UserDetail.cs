using System;
using System.Collections.Generic;

#nullable disable

namespace SportsClub.Models
{
    public partial class UserDetail
    {
       

        public long Id { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
  

        public virtual User User { get; set; }



    }
}
