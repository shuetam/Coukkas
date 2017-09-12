using System;
using Coukkas.Core.Domain;

namespace Coukkas.Infrastructure.FromBodyCommands
{
    public class FenceCreated
    {
        public Guid ID {get;}
        public string Name {get;  set;}
        public string Description {get; set;}
        public int Days {get; set;}
        public double lat {get; set;}
        public double lon {get; set;}
        public double Radius {get; set;}

    }
}