using System;
using System.Collections.Generic;
using System.Timers;

namespace Coukkas.Core.Domain
{
    public class FactTryCatchCoupon
    {

        public int ID {get; set;}
        public DateTime TryDate {get; protected set;}
        public Location location{get; protected set;}
        public string Category {get; protected set;}


        public FactTryCatchCoupon()
        {}
        public FactTryCatchCoupon(DateTime date, Location location, string Category)
        {
            this.TryDate = date;
            this.location = location;
            
            if (Categories.Names.Contains(Category))
            {
            this.Category = Category;
            }
            else
            {
              this.Category = "undefined";   
            }
        }
    }
}