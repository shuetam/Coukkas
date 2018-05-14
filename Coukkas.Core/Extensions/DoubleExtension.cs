using System;

namespace Coukkas.Core
{
    public  static class DoubleExtension
    {
        public static double ToDegGeo(this double @double) // in meters // za;ezne od szerokosci geo
        {
          var R = 6378137;
          //double R = 6371000;
           // var R = 6378137*Math.Sin(lat.ToRadian());
            return (@double *360)/(2*Math.PI*R);
        }

public static double ToDegGeoLat(this double @double, double lat) // in meters // za;ezne od szerokosci geo
        {
           // var R = 6378137; 
          // double R = 6371000;
            var R = (6378137)*Math.Sin(lat.ToRadian());
            return (@double *360)/(2*Math.PI*R);
        }


        public static double ToRadian(this double deg)
        {
            return (Math.PI / 180) * deg;
        }

         public static double ToMeters(this double @double) 
        {
            var R = 6378137;
            return (@double*2*Math.PI*R)/360;
        }
    }
}
