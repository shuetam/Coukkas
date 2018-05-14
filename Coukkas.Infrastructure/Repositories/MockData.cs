using System;
using System.Threading.Tasks;
using Coukkas.Core;
using Coukkas.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coukkas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Coukkas.Infrastructure.Repositories;

namespace Coukkas.Infrastructure
{

    public class MockData
    {
     public static Location GetMockLocation(MockFence fence)
        {
            Random random = new Random();

            double lat = fence.Latitude.ToRadian();

            double x = DoubleLottery() * fence.Radius;
            var longitude = x/111111  + fence.Longitude;    

            double y = Math.Sqrt(Math.Pow(fence.Radius,2) - Math.Pow(x,2));
            var latitude = (y * DoubleLottery())/111111*Math.Cos(fence.Latitude.ToRadian()) + fence.Latitude;
            
            return new Location(latitude,longitude);
        }


      private static double DoubleLottery()
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