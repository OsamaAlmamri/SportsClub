using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SportsClub.Models;

namespace SportsClub.Core.Requests
{
    public class UserSubscriptionRequest
    {
        [Required]
        public List<UserSubscriptionServicesRequest> Services { get; set; }




    }

    public class UserSubscriptionServicesRequest
    {
        [Required]
        public long ServiceId { get; set; }
 
        public DateTime? StartAt { get; set; }
    }

    public class OneServiceValidator : AbstractValidator<UserSubscriptionServicesRequest>
    {
        public OneServiceValidator(SportsClubContext database)
        {
            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Service Id  is required!");

           RuleFor(w => w.ServiceId)
         .Must(ServiceId => database.Services.Any(type => type.Id == ServiceId))
         .WithMessage("Service  does not exist with id ${ServiceTimeId}");
        }
    }

    public class ServicessValidator : AbstractValidator<UserSubscriptionRequest>
    {
        public ServicessValidator(SportsClubContext database)
        {
            RuleForEach(x => x.Services).SetValidator(new OneServiceValidator(database));
        }
    }
}
