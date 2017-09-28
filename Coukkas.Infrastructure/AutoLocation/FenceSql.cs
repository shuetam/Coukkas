using System;
using Coukkas.Core.Domain;
using Coukkas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;

namespace Coukkas.Infrastructure.EntityFramework
{
    public class FenceSql
    {
        public string ID {get; protected set;}
        public double? Radius {get; protected set;}
        public double? Latitude {get; protected set;}
        public double? Longitude {get; protected set;} 
    

    public FenceSql (string id, double? radius, double? latitude, double? longitude)
    {
        ID = id;
        Radius = radius;
        Latitude = latitude;
        Longitude = longitude;
    }
    }
}

        
