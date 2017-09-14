using System;
using System.Collections.Generic;

namespace Coukkas.Core.Domain
{
    public class User: Entity
    {
        public string Role {get; protected set;}
        public string Name {get; protected set;}
        public string Email {get; protected set;}
        public string Password {get; protected set;}
        public DateTime CreatedDate {get; protected set;}
        public Location Location {get; protected set;}

        private  ISet <Coupon> _coupons = new HashSet<Coupon>();
        public IEnumerable <Coupon> Coupons {get=> _coupons;} 

        protected User()
        {}

        public User(Guid Id, string email, string name, string password, string role)
        {
            this.Id = Id;
            Role = role;
            Name = name;
            Email = email;
            Password = password;
            CreatedDate = DateTime.UtcNow;
        }

        public void SetLocation(double lat, double lon)
        {   
            this.Location = new Location(lat, lon);
        }

        public void AddCatchedCoupon(Coupon coupon)
        {
            _coupons.Add(coupon);
        }
    }
}

            