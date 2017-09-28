using Coukkas.Core.Domain;
using Coukkas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;
using System;
using Coukkas.Core;

namespace Coukkas.Infrastructure.EntityFramework
{
    public class CouponSql
    {
        public string ID {get; protected set;}
        public string FenceID {get; protected set;}
        public int? LocationID {get; protected set;}
        public double? Latitude {get; protected set;}
        public double? Longitude {get; protected set;}
    

    public CouponSql (string id, string fenceId, int? locationId, double? latitude, double? longitude)
    {
        ID = id;
        FenceID = fenceId;
        LocationID = locationId;
        Latitude = latitude;
        Longitude = longitude;
    }


        public void ChangeCouponLocation(FenceSql Fence)
        {
            Random random = new Random();
            this.Latitude = LocationLottery.Invoke(Fence).ToDegGeo() +  Fence.Latitude;
            this.Longitude = LocationLottery.Invoke(Fence).ToDegGeo() + Fence.Longitude;
        }

      private Func <FenceSql, double> LocationLottery = (f) => 
      {
        Random ran = new Random();
        int w = ran.Next(0,2);
        if (w == 0)
        {return ran.NextDouble()*f.Radius.Value;}
        else
        {return ran.NextDouble()*f.Radius.Value * (-1);}
      };  

    }
}