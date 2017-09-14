using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Coukkas.Core;
using Coukkas.Core.Domain;
using Coukkas.Infrastructure.Repositories.DTOS;

namespace Coukkas.Infrastructure.Services
{
    public class CouponService : ICouponService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFenceRepository _fenceRepository;
        private readonly IMapper _autoMapper;
        public CouponService( IFenceRepository fenceRepository, IUserRepository userRepository, IMapper autoMapper)
        {
            _fenceRepository = fenceRepository;
            _userRepository = userRepository;
            _autoMapper = autoMapper;
        }

        public async Task <List<CouponDto>> GetAvailableCouponsAsync(Guid UserId)
        {
            var user = await _userRepository.GetAsync(UserId);
            var avaibleFences = await _fenceRepository.GetAvailableAsync(user.Location);
          
            var coupons = new List<Coupon>();

            foreach(var f in avaibleFences)
            {
                coupons.AddRange(f.AvaibleCoupons.Where(c 
                => c.location.GetDistanceTo(user.Location)<2));
            }
           // return coupons.Select( f => _autoMapper.Map<CouponDto>(f)).ToList();
           return _autoMapper.Map<List<CouponDto>>(coupons);
        }

        public async Task CatchCoukka(Guid UserId, int couponIndex)
        {
            var user = await _userRepository.GetAsync(UserId);
            var avaibleFences = await _fenceRepository.GetAvailableAsync(user.Location);
          
            var coupons = new List<Coupon>();

            foreach(var f in avaibleFences)
            {
                coupons.AddRange(f.AvaibleCoupons.Where(c 
                => c.location.GetDistanceTo(user.Location)<2));
            }

            var coupon = coupons[couponIndex];
            coupon.Catch(user);
        }
    }
}