using System;
using System.Collections.Generic;
using AutoMapper;
using Coukkas.Core.Domain;
using Coukkas.Infrastructure.Repositories.DTOS;
using System.Linq;


namespace Coukkas.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(
                config =>
                {
                config.CreateMap<Fence, FenceDto>()
                .ForMember(f => f.AvaibleCouponsCount, 
                m => m.MapFrom(p => p.Coupons.Where(x=>!x.Caught).Count()));
                config.CreateMap<User, AccountDto>();
                config.CreateMap<Coupon, CouponDto>();
                
                config.CreateMap<Fence, FenceData>()
                .ForMember(f => f.Longitude, m => m.MapFrom(p => p.location.Longitude))
                .ForMember(f => f.Latitude , m => m.MapFrom(p => p.location.Latitude));

                config.CreateMap<Coupon,CouponsData>()
                .ForMember(c => c.Longitude, m => m.MapFrom(p => p.location.Longitude))
                .ForMember(c => c.Latitude , m => m.MapFrom(p => p.location.Latitude))
                .ForMember(c => c.Category, m => m.MapFrom(co => co.fence.Category));




                }
            ).CreateMapper();
        }
    }
}