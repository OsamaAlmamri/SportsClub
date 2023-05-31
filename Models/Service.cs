using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable
using FluentValidation;
namespace SportsClub.Models
{
    public partial class Service
    {

        public Service()
        {
            UserSubscriptionServices = new HashSet<UserSubscriptionService>();
        }

        public long Id { get; set; }
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
        public DateTime CreatedAt { get; set; }




        public virtual ServiceTime ServiceTime { get; set; }
        public virtual ServiceType ServiceType { get; set; } = null!;
        public virtual ICollection<UserSubscriptionService> UserSubscriptionServices { get; set; }



    }

    public class ServiceForignKeysValidator : AbstractValidator<Service>
    {
        public ServiceForignKeysValidator(SportsClubContext database)
        {
            this.RuleFor(w => w.ServiceTypeId)
                .Must(ServiceTypeId => database.ServiceTypes.Any(type => type.Id == ServiceTypeId))
                .WithMessage("Service Type does not exist with id ${ServiceTimeId}");

            this.RuleFor(w => w.ServiceTimeId)
              .Must(ServiceTimeId => database.ServiceTimes.Any(type => type.Id == ServiceTimeId))
              .WithMessage("Service time does not exist");

        }
    }
}
