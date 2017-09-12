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

namespace Coukkas.Api.Controllers
{
    
    public class CouponController : ValuesController
    {
         private readonly  IUserService _userService;
        private readonly  IFenceService _fenceService;

        public CouponController (IUserService userService, IFenceService fenceService)
        {
            _userService = userService;
            _fenceService = fenceService;
        }

        [HttpPut("{fenceId}")]
        [Authorize]
        public async Task <IActionResult> AddCoupons(Guid fenceId, [FromBody] CouponCreated command) 
        {
            await _fenceService.AddCoupons(fenceId, command.Discount, command.amount, command.EndOfValidity);
            return Created("/coupon", null);
        }
    }
}