using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Timers;

namespace Coukkas.Core.Domain
{
    public class MockFence
    {
        public double Radius {get;  set;}
        public double Latitude {get;  set;}
        public double Longitude {get;  set;}

        public MockFence(double rad, double lat, double lon)
        {
            this.Radius = rad;
            this.Latitude = lat;
            this.Longitude = lon;
        }

    }

}