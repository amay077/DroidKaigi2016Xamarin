using System;
using Java.Util;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class DateTimeExtension
    {
        public static Date ToJavaDate(this DateTime self)
        {
            return new Date(self.ToUnixMilliSeconds());
        }

        public static long ToUnixMilliSeconds(this DateTime self)
        {
            return (long)(self.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
        }
    }
}

