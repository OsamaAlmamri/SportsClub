using System;
using System.Collections.Generic;
using SportsClub.Models;
using System.ComponentModel.DataAnnotations;
#nullable disable
using FluentValidation;
namespace SportsClub.Core.Requests
{

    public  class  ServiceRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Period { get; set; }
        [Required]
        public long ServiceTypeId { get; set; }
     
        public long? ServiceTimeId { get; set; }

        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
    public partial class ServiceForignKeysValidator : AbstractValidator<ServiceRequest>
    {
        public ServiceForignKeysValidator(SportsClubContext database)
        {
            this.RuleFor(w => w.ServiceTypeId)
                
                .Must(ServiceTypeId => database.ServiceTypes.Any(type => type.Id == ServiceTypeId))
                .WithMessage("Service Type does not exist with id ${ServiceTimeId}");

         
         /*   this.RuleFor(w => w.ServiceTimeId)
              .Must(ServiceTimeId => database.ServiceTimes.Any(type => type.Id == ServiceTimeId))
              .WithMessage("Service time does not exist");*/

        }
    }
}
