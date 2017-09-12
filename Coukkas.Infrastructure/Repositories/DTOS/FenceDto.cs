using System;
using System.Collections.Generic;
using System.Linq;
using Coukkas.Core;
using Coukkas.Core.Domain;

namespace Coukkas.Infrastructure
{
    public class FenceDto
    {
        public Guid Id {get; set;}
        public Guid OwnerID {get; set;}
        public string Name {get;  set;}
        public string Description {get; set;}
        public int AvaibleCouponsCount {get; set;}
    }
}