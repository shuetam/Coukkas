using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Timers;

namespace Coukkas.Core.Domain
{
    public class Fence: Entity
    {
        public DateTime CreatedAt {get; protected set;}
        public string Name {get; protected set;}
        public string Description {get; set;}
        public DateTime EndDate {get; protected set;}
        public Location location{get; protected set;}
        public double Radius {get; protected set;}
        public Guid OwnerID {get; protected set;}

        public string Category {get; protected set;}
        
        public List<Coupon> Coupons {get; protected set;}
        

        protected Fence()
        {}

        public Fence (Guid Id, Guid OwnerId, string Name, string Description,  string Category, 
        DateTime CreatedAt, DateTime EndDate, double lat, double lon, double Radius)
        {
            this.Id = Id;
            this.OwnerID = OwnerId;
            this.Name = Name;
            this.Description = Description;
            this.CreatedAt = DateTime.Now;
            this.EndDate = EndDate;
            this.location = new Location(lat, lon);
            this.Radius = Radius;
            if (Categories.Names.Contains(Category))
            {
            this.Category = Category;
            }
            else
            {
              this.Category = "undefined";   
            }
           
        }
        
        public void AddCoupons (int amount)
        {
            for(int i = 0;i<amount;i++)
            {
                Coupons.Add(new Coupon(Guid.NewGuid(),this));
            }
        }
    }
} 