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

         await _fenceService.CreateAsync
         (Guid.NewGuid(), UserId, fence.Name,fence.Description,DateTime.UtcNow, DateTime.UtcNow.AddDays(fence.Days), fence.lat, fence.lon, fence.Radius);

           return Created($"/{UserId}/fences", null);
        } 

        [HttpGet("fences")]
        [Authorize]
        public async Task <IActionResult> GetAllFances()
        {
            var fences = await _fenceService.GetNotAvailableAsync(UserId);
            return Json(JsonConvert.SerializeObject(fences));
        }

        [HttpGet("myfences")]
        [Authorize]   // dla roli company
        public async Task <IActionResult> GetFences()
        {   
            var fences = await _fenceService.GetByOwnerAsync(UserId);
            return Json(JsonConvert.SerializeObject(fences));
            
        }
    }
}