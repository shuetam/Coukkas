using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Coukkas.Infrastructure;

namespace Coukkas.Api.Controllers
{
    [Route("[controller]")]
    public class ValuesController : Controller
    {
    protected Guid UserId
    {
        get
        { 
        if (User.Identity.IsAuthenticated)
            {return Guid.Parse(User.Identity.Name);
        }
        else
        {
            return Guid.Empty;
        }
        }
    }
    }
}

