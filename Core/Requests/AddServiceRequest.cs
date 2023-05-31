using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using SportsClub.Models;

namespace SportsClub.Core.Requests
{
    public class AddServiceRequest
    {
      

        [Required]
        public string? Name { get; set; }
        [Required]
        public int Period { get; set; }
        [Required]
        public long ServiceTypeId { get; set; }
        [Required]
        public long? ServiceTimeId { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
    }


  
}
