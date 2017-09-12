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



/* 
    {
    if(User.Identity.IsAuthenticated)
    {
   return Guid.Parse(User.Identity.Name);
    }


    }
    
    };
    }

        protected Guid UserId =>
        {
            if (User.Idenity.Is

        }





        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
 */