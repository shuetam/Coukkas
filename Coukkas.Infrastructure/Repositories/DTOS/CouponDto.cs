using System;
using System.Collections.Generic;
using System.Linq;
using Coukkas.Core;
using Coukkas.Core.Domain;

namespace Coukkas.Infrastructure.Repositories.DTOS
{
    public class CouponDto
    {
        public Guid Id {get; set;}
    
        public Guid FenceId { get; set;}
    }

}