using System;
//using System.Threading;
using System.Timers;




namespace Coukkas.Core.Domain
{
    public class Coupon : Entity
    {
        
        public Guid FenceId { get; protected set;}
        public double Discount {get; protected set;}
        public DateTime EndOfValidity {get; protected set;}
        public Guid? UserId {get; protected set;}
        public Location location {get; protected set;}
        public bool Caught => UserId.HasValue;



        protected Coupon()
        {}

        public Coupon(Fence fence, double discount, DateTime endOfValidity)
        {
            FenceId = fence.Id;
            Discount = discount;
            EndOfValidity = endOfValidity;
           // SetLocation(fence); // in future on server
        }

        Timer timer;
        private void SetLocation(Fence fence)
        {
            Random random = new Random();
            timer = new Timer();
            timer.Interval= TimeSpan.FromMinutes(30).TotalMilliseconds;
            timer.Elapsed += (sender, e) => 
          {
            this.location.Latitude = LocationLottery.Invoke(fence).ToDegGeo() + fence.location.Latitude;
            this.location.Longitude = LocationLottery.Invoke(fence).ToDegGeo() + fence.location.Longitude;
          };
        
        timer.Start();
        }

      public void Catch (User user)
      {
          this.UserId = user.Id;
          timer.Stop();
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