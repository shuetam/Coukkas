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



//  1° of Longitude = 111.41288 * cos(theta) - 0.09350 * cos(3 * theta) + 0.00012 * cos(5 * theta)


        public void ChangeCouponLocation(FenceSql Fence)
        {
            Random random = new Random();

            double lat = Fence.Latitude.Value.ToRadian();

            double x = DoubleLottery() * Fence.Radius.Value;
            this.Longitude = x/111111  + Fence.Longitude;    

            double y = Math.Sqrt(Math.Pow(Fence.Radius.Value,2) - Math.Pow(x,2));

           

            this.Latitude = (y * DoubleLottery())/111111*Math.Cos(Fence.Latitude.Value.ToRadian()) + Fence.Latitude;
            
        }
 
      private double DoubleLottery()
      {
        Random ran = new Random();
        int w = ran.Next(0,2);
        if (w == 0)
        {return ran.NextDouble();}
        else
        {return (ran.NextDouble() * (-1));}
      } 

    }
}