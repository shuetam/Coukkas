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
    
    public class FenceController : ValuesController
    {
        private readonly  IUserService _userService;
        private readonly  IFenceService _fenceService;

        public FenceController (IUserService userService, IFenceService fenceService)
        {
            _userService = userService;
            _fenceService = fenceService;
        }

        [HttpPost("create")]
        [Authorize]
        
        public async Task <IActionResult> CreateFence([FromBody] FenceCreated fence)
        {
            Guid fenceID = Guid.NewGuid();
         await _fenceService.CreateAsync
         (fenceID, UserId, fence.Name,fence.Description,DateTime.UtcNow, DateTime.UtcNow.AddDays(fence.Days), fence.lat, fence.lon, fence.Radius);

           return Created($"fences/{fenceID}", null);
        } 

        [HttpGet("outfences")]
        [Authorize]
        public async Task <IActionResult> GetOutFances()
        {
            var fences = await _fenceService.GetNotAvailableAsync(UserId);
            return Json(fences);
        }

        [HttpGet("infences")]
        [Authorize]
        public async Task <IActionResult> GetInFances()
        {
            var fences = await _fenceService.GetAvailableAsync(UserId);
            return Json(fences);
        }

        [HttpGet("myfences")]
        [Authorize]   // dla roli company
        public async Task <IActionResult> GetFences()
        {   
            var fences = await _fenceService.GetByOwnerAsync(UserId);
            //return Json(JsonConvert.SerializeObject(fences, Formatting.Indented).ToString()); 
             return Json(fences); 
              
        }

        [HttpPost("addcoupons")]
        [Authorize]  // dla roli company
        public async Task <IActionResult> AddCoupons([FromBody] CouponCreated command) 
        {
            await _fenceService.AddCoupons(command.FenceId, command.Discount, command.amount, command.EndOfValidity);
            return Created("/coupon", null);
        }
    }
}