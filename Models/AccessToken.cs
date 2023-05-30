using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsClub.Models
{
    public class AccessToken
    {
        public string Value { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
