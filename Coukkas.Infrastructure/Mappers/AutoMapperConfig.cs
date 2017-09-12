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
                .ForMember(f => f.AvaibleCouponsCount, m => m.MapFrom(p => p.AvaibleCoupons.Count()));
                config.CreateMap<User, AccountDto>();
                config.CreateMap<Coupon, CouponDto>();
                }
            ).CreateMapper();
        }
    }
}