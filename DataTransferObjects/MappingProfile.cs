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
               
                .ForMember(v => v.ServiceTypeName, opt => opt.MapFrom(vr => vr.ServiceType.Name))
                .ForMember(v => v.ServiceTypeName, opt => opt.MapFrom(vr => vr.ServiceType.Name))
                .ForMember(v => v.FromTime, opt => opt.MapFrom(vr => vr.ServiceTime.FromTime))
                .ForMember(v => v.ToTime, opt => opt.MapFrom(vr => vr.ServiceTime.ToTime));
            CreateMap<UserDetail, UserDetailDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserSubscription, UserSubscriptionDto>();
            CreateMap<UserSubscriptionPaymentGatway, UserSubscriptionPaymentGatwayDto>();
            CreateMap<UserSubscriptionService, UserSubscriptionServiceDto>();
           




        }
    }
}
