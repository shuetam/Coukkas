using System;
//using System.Threading;
using System.Timers;




namespace Coukkas.Core.Domain
{
    public class Coupon : Entity
    {
        
        public Guid FenceId { get; protected set;}
        public Fence Fence { get; set;}
        public double Discount {get; protected set;}
        public DateTime EndOfValidity {get; protected set;}
        public Guid? UserId {get; protected set;}
        public Location location {get; protected set;}
        public bool Caught => UserId.HasValue;

       
        protected Coupon()
        {}

        public Coupon(Guid id,  Fence fence, double discount, DateTime endOfValidity)
        {
          this.Id = id;
            FenceId = fence.Id;
            Discount = discount;
            EndOfValidity = endOfValidity;
            Fence = fence;
            this.location = new Location(34,34);
        }
            

        public void ChangeCouponLocation()
        {
            Random random = new Random();
            this.location.Latitude = LocationLottery.Invoke(Fence).ToDegGeo() + Fence.location.Latitude;
            this.location.Longitude = LocationLottery.Invoke(Fence).ToDegGeo() + Fence.location.Longitude;
        }

      public void Catch (User user)
      {
          this.UserId = user.Id;
          user.AddCatchedCoupon(this);
      }
          
  
      private Func <Fence, double> LocationLottery = (f) => 
      {
        Random ran = new Random();
        int w = ran.Next(0,2);
        if (w == 0)
        {return ran.NextDouble()*f.Radius;}
        else
        {return ran.NextDouble()*f.Radius * (-1);}
      };  
    }
}