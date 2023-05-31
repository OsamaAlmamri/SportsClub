using SportsClub.Models;
using System;

namespace SportsClub.DataTransferObjects
{
    public class AuthUserDto
    {

        public string Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? PhoneNumber { get; set; }

        public string FullName { get; set; }
        public string? Address { get; set; }
        public DateTime BirthDate { get; set; }


    }
}
