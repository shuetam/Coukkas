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
    
    public class MockDataController : ValuesController
    {

        private readonly  IUserService _userService;
        public MockDataController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("{i}")]
        public async Task <IActionResult> FillFacts(int i)
        {
            await _userService.FillRandomFacts(i);
            return Created("/mockdata", null);
        } 
    }
}