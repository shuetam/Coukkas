using System;
using System.Collections.Generic;
using System.Linq;
using Coukkas.Core;
using Coukkas.Core.Domain;

namespace Coukkas.Infrastructure
{
    public class TokenDto
    {
        public string Token {get; set;}
        public DateTime Expire {get; set;}
        public string Role {get; set;}
    }

}