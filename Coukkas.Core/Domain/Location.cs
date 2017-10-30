using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coukkas.Core.Domain
{
    public class Location
    {
        public double? Latitude {get;  set;}
        public double? Longitude {get;  set;}
        public int ID {get; set;}
  
public Location()
{}  
    public Location ( double Lat, double Long)
    {
        this.Latitude = Lat;
        this.Longitude = Long; 
    }

    public double GetDistanceTo(Location location2) // in meters
    {
            var R = 6378137;
          
            double lat1=this.Latitude.Value;
            double long1=this.Longitude.Value;

            double lat2=location2.Latitude.Value;
            double long2=location2.Longitude.Value;

            var dLat = (lat2 - lat1).ToRadian();
            var dLon = (long2 - long1).ToRadian();

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos((lat1).ToRadian()) * Math.Cos((lat2).ToRadian()) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d;        
    }

 

    }
}