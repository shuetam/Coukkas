using System;

namespace Coukkas.Core
{
    public  static class DateTimeExtension
    {
        public static long ToUnixTime(this DateTime dateTime)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan time = dateTime.Subtract(date); 
            return time.Ticks/10000;
        }
    }
}

