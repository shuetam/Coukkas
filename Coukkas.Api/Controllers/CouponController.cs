using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Coukkas.Infrastructure;
using Coukkas.Infrastructure.Services;
using Coukkas.Infrastructure.Repositories.DTOS;
using Microsoft.AspNetCore.Authorization;
using Coukkas.Infrastructure.FromBodyCommands;
using Coukkas.Core.Domain;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Coukkas.Api.Controllers
{
    
    public class CouponController : ValuesController
    {
         private readonly  IUserService _userService;
        private readonly  IFenceService _fenceService;
        private readonly  ICouponService _couponService;

        public CouponController (IUserService userService, IFenceService fenceService, ICouponService couponService)
        {
            _userService = userService;
            _fenceService = fenceService;
            _couponService = couponService;
        }

        [HttpGet]
        [Authorize]
        public async Task <IActionResult> GetCoupons()
        {
            var coupons = await _couponService.GetAvailableCouponsAsync(UserId);
            if (coupons == null)
            {
                throw new Exception("There are no available coukkas in your vicinity.");
            }
            return Json(coupons);
        }

        [HttpPut("{couponIndex}")]
        [Authorize]
        public async Task <IActionResult> CatchCoukka(int couponIndex)
        {
            await _couponService.CatchCoukka(UserId, couponIndex);
            return NoContent();
        }


    }
}