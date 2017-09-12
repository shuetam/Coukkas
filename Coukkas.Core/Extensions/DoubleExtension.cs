using System;

namespace Coukkas.Core
{
    public  static class DoubleExtension
    {
        public static double ToDegGeo(this double @double) // in meters
        {
            var R = 6378137;
            return (@double *360)/(2*Math.PI*R);
        }


        public static double ToRadian(this double deg)
        {
            return (Math.PI / 180) * deg;
        }
    }
}
