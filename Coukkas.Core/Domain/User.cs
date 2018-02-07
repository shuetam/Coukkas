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
            SetLogin(name);
            SetEmail(email);
            SetPassword(password);
            CreatedAt = DateTime.UtcNow;
            this.Location = new Location();
        }

        private void SetLogin(string login)
        {
            if(login.IsLoginMatch())
            {this.Name = login;}
            else 
            {throw new Exception("Wrong login format");}
        }

            private void SetEmail(string email)
        {
            if(email.IsEmailMatch())
            {this.Email = email;}
            else 
            {throw new Exception("Wrong email format");}
        }

        private void SetPassword(string password)
        {
            this.Password = password;
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
        


            