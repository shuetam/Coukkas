using System;
using System.Collections.Generic;
using System.Linq;
using Coukkas.Core;
using Coukkas.Core.Domain;

namespace Coukkas.Infrastructure
{
    public class FenceData
    {
        public string Name {get;  set;}
        public string Category {get; set;}
        public double Radius {get; set;}
        public double? Latitude {get;  set;}
        public double? Longitude {get;  set;}
    }
}