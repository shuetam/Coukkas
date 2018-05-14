using System;
using Coukkas.Core.Domain;

namespace Coukkas.Infrastructure.FromBodyCommands
{
    public class CouponCreated
    {
       
       
        public Guid FenceId{get; set;}
      //  public double Discount {get; set;}
      //  public DateTime EndOfValidity {get; set;}
        public  int amount {get; set;}
    }
}