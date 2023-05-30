using System;
using System.Collections.Generic;

#nullable disable

namespace SportsClub.Models
{
    public partial class RefreshToken
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
