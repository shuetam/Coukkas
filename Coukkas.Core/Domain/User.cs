using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Coukkas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Coukkas.Core.Domain
{
    public class User: Entity
    {
        public string Role {get; protected set;}
        public string Name {get; protected set;}
        public string Email {get; protected set;}
        public string Password {get; protected set;}
        
        public DateTime CreatedAt {get; protected set;}
       
        public Location Location {get;  set;}
        
        
        private  ISet <Coupon> _coupons = new HashSet<Coupon>();
        
        public  IEnumerable <Coupon> Coupons {get=> _coupons;} 
        protected User()
        {}
        public User(Guid Id, string email, string name, string password, string role)
        {
            this.Id = Id;
            Role = role;
            Name = name;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            this.Location = new Location();
        }

        public void SetLocation(double lat, double lon)
        {   
            this.Location.Latitude = lat;
            this.Location.Longitude = lon;
        }

        public void AddCatchedCoupon(Coupon coupon)
        {
            _coupons.Add(coupon);
        }
    }
}

            