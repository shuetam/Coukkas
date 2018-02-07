using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coukkas.Core.Domain;
using Coukkas.Infrastructure.Repositories.DTOS;

namespace Coukkas.Infrastructure.Services
{
    public interface ICouponService
    {
        Task <List<CouponDto>> GetAvailableCouponsAsync (Guid UserId);
         Task CatchCoukka(Guid UserId, int couponIndex);
    }
}