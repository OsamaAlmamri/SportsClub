using AutoMapper;
using SportsClub.Models;
using SportsClub.DataTransferObjects;

using System.Linq;

namespace SportsClub.DataTransferObjects
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PaymentGatway, PaymentGatwayDto>();
            CreateMap<ServiceType, ServiceTypeDto>();
        
            CreateMap<ServiceTime, ServiceTimeDto>();
            CreateMap<Service, ServiceDto>()
               
                .ForMember(v => v.ServiceTypeName, opt => opt.MapFrom(vr => vr.ServiceType.Name));
            CreateMap<UserDetail, UserDetailDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserSubscription, UserSubscriptionDto>();
            CreateMap<UserSubscriptionPaymentGatway, UserSubscriptionPaymentGatwayDto>();
            CreateMap<UserSubscriptionService, UserSubscriptionServiceDto>();
           




        }
    }
}
