using System;

namespace Hdp.CoreRx.Extensions
{
    public static class DateTimeExtensions
    {
        public static int ToUnixTimestamp(this DateTime value)
        {
            return (int)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }

//        public static DateTime UnixTimeStampToDateTime( double unixTimeStamp )
//        {
//            // Unix timestamp is seconds past epoch
//            System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
//            dtDateTime = dtDateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
//            return dtDateTime;
//        }
    }
}

