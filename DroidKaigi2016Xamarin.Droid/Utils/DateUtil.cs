using System;
using Android.OS;
using Android.Text.Format;
using Java.Text;
using Java.Util;
using Android.Content;
using Java.Util.Concurrent;

namespace DroidKaigi2016Xamarin.Droid.Utils
{
    public static class DateUtil 
    {
        private static readonly string FORMAT_MMDD = "MMMd";
        private static readonly string FORMAT_KKMM = "kk:mm";
        private static readonly string FORMAT_YYYYMMDDKKMM = "yyyyMMMdkkmm";

        public static string GetMonthDate(Date date, Context context) 
        {
            return GetMonthDate(date, AppUtil.GetCurrentLocale(context), context);
        }

        public static string GetMonthDate(Date date, Locale locale, Context context) 
        {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBeanMr2) 
            {
                var pattern = Android.Text.Format.DateFormat.GetBestDateTimePattern(locale, FORMAT_MMDD);
                var sdf = new SimpleDateFormat(pattern, locale);
                return sdf.Format(date);
            } 
            else 
            {
                var flag = FormatStyleFlags.ShowDate | FormatStyleFlags.NoYear;
                return DateUtils.FormatDateTime(context, date.Time, flag);
            }
        }

        public static string GetHourMinute(Date date) 
        {
            return Android.Text.Format.DateFormat.Format(FORMAT_KKMM, date);
        }

        public static string GetLongFormatDate(Date date, Context context) 
        {
            var locale = AppUtil.GetCurrentLocale(context);
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBeanMr2) 
            {
                var pattern = Android.Text.Format.DateFormat.GetBestDateTimePattern(locale, FORMAT_YYYYMMDDKKMM);
                return new SimpleDateFormat(pattern, locale).Format(date);
            } 
            else 
            {
                var sdf = Java.Text.DateFormat.GetDateInstance(Java.Text.DateFormat.Long, locale) as SimpleDateFormat;
                return sdf.Format(date) + GetHourMinute(date);
            }
        }
            
        public static int GetMinutes(Date stime, Date etime) 
        {
            var range = etime.Time - stime.Time;

            if (range > 0) 
            {
                return (int) (range / TimeUnit.Minutes.ToMillis(1L));
            } 
            else 
            {
                return 0;
            }
        }

    }
}

