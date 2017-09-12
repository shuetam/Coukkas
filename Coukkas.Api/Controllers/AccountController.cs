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

namespace Coukkas.Api.Controllers
{
    
    public class AccountController : ValuesController
    {
        private readonly  IUserService _userService;
        public AccountController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task <IActionResult> Register([FromBody] UserRegister user)
        {
           await _userService.RegisterAsync(Guid.NewGuid(), user.Email, user.Name, user.Password, user.Role);
           return Created($"/account/{user.Name}", null);
        } 

        [HttpPost("login")]
        public async Task <IActionResult> Login([FromBody] UserLogin user)
        {
           return Json( await _userService.LoginAsync(user.Email, user.Password));
        } 

        [HttpGet]
        [Authorize]
        public async Task <IActionResult> GetUser()
        {
           return Json( await _userService.GetAccountAsync(UserId));
           
        } 

        [HttpPost("location")]
        [Authorize]
        public async Task <IActionResult> SetUserLocation([FromBody] LocationSetter location)
        {
           await _userService.SetLocation(this.UserId, location.Latitude, location.Longitude);
           return Created($"/account/{UserId}/location", null);
        } 
    }
}