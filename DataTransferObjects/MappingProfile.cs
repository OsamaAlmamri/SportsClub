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
            CreateMap<User, AuthUserDto>()
               
                .ForMember(v => v.FullName, opt => opt.MapFrom(vr => vr.UserDetail.FullName))
                .ForMember(v => v.Address, opt => opt.MapFrom(vr => vr.UserDetail.Address))
                .ForMember(v => v.BirthDate, opt => opt.MapFrom(vr => vr.UserDetail.BirthDate));
            CreateMap<UserSubscriptionService, UserSubscriptionServiceDto>()
                .ForMember(v => v.Service, opt => opt.MapFrom(vr => vr.Service));
            
              ;
            CreateMap<UserSubscription, UserSubscriptionDetailsDto>()

                //   .ForMember(v => v.Services, opt => opt.MapFrom(vr => vr.UserSubscriptionServices));
                // .ForMember(v => v.payments, opt => opt.MapFrom(vr => vr.UserServicePaymentGatways))
                .ForMember(v => v.User, opt => opt.MapFrom(vr => vr.User));
               
            CreateMap<UserDetail, UserDetailDto>();
            CreateMap<User, UserDto>()
                   .ForMember(v => v.FullName, opt => opt.MapFrom(vr => vr.UserDetail.FullName))
                .ForMember(v => v.Address, opt => opt.MapFrom(vr => vr.UserDetail.Address))
                .ForMember(v => v.BirthDate, opt => opt.MapFrom(vr => vr.UserDetail.BirthDate));
            CreateMap<UserSubscription, UserSubscriptionDto>();
            CreateMap<UserSubscriptionPaymentGatway, UserSubscriptionPaymentGatwayDto>();
        




        }
    }
}
