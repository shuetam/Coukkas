using System;
using System.Collections.Generic;
using System.Linq;

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
        private  ISet <Coupon> _coupons = new HashSet<Coupon>();
        public IEnumerable<Coupon> Coupons {get=> _coupons;} 
        public IEnumerable<Coupon> AvaibleCoupons => Coupons.Where(c=> !c.Caught);
        public IEnumerable<Coupon> NoAvaibleCoupons => Coupons.Where(c=> c.Caught);

        public Fence (Guid Id, Guid OwnerId, string Name, string Description, DateTime CreatedAt, DateTime EndDate, double lat, double lon, double Radius)
        {
            this.Id = Id;
            this.OwnerID = OwnerId;
            this.Name = Name;
            this.Description = Description;
            this.CreatedAt = DateTime.Now;
            this.EndDate = EndDate;
            this.location = new Location(lat, lon);
            this.Radius = Radius;
        }
        
        public void AddCoupons (int amount, double discount, DateTime end)
        {
            for(int i = 0;i<amount;i++)
            {
                _coupons.Add(new Coupon(Guid.NewGuid(), this, discount, end));
            }
        }
    }
} 