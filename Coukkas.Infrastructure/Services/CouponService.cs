using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
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

            var timer = new Timer();
            timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
                   
           timer.Elapsed += (sender, e) =>
          {

             _fenceRepository.UpdateAllAsync();
          };

            timer.Start();
            
        }

        public async Task <List<CouponDto>> GetAvailableCouponsAsync(Guid UserId)
        {
            var user = await _userRepository.GetAsync(UserId);
            var location = user.Location;
            var avaibleFences = await _fenceRepository.GetAvailableAsync(location);
          
            var coupons = new List<Coupon>();

            foreach(var f in avaibleFences)
            {
                coupons.AddRange(f.Coupons.Where(x=>!x.Caught).Where
                (c => c.location.GetDistanceTo(location)<2));
            }

           return _autoMapper.Map<List<CouponDto>>(coupons);
        }

        public async Task CatchCoukka(Guid UserId, int couponIndex)
        {
            var user = await _userRepository.GetAsync(UserId);
            var location = user.Location;
            var avaibleFences = await _fenceRepository.GetAvailableAsync(location);
          
            var coupons = new List<Coupon>();

            foreach(var f in avaibleFences)
            {
                coupons.AddRange(f.Coupons.Where(x=>!x.Caught).Where
                (c => c.location.GetDistanceTo(location)<2));
            }

            var coupon = coupons[couponIndex];
            coupon.Catch(user);
        }
    }
}