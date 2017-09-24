using System;
using System.Collections.Generic;
using Coukkas.Core.Domain;

namespace Coukkas.Infrastructure.Repositories.DTOS
{
    public class AccountDto
    {
        public Guid Id {get; set;}
        public string Role {get;  set;}
        public string Name {get;  set;}
        public string Email {get;  set;}
        public Location Location {get; set;}

        public IEnumerable <CouponDto> Coupons {get; set;}
      
    }
}