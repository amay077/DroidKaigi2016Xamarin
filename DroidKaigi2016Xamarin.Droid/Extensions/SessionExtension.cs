using System;
using DroidKaigi2016Xamarin.Core.Models;
using Android.Content;
using DroidKaigi2016Xamarin.Droid.Utils;
using Android.Support.V4.Content;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class SessionExtension
    {
//        @BindingAdapter("sessionTimeRange")
        public static string GetSessionTimeRange(this Session session, Context context) 
        {
            return context.GetString(Resource.String.session_time_range,
                DateUtil.GetHourMinute(session.stime.ToJavaDate()),
                DateUtil.GetHourMinute(session.etime.ToJavaDate()),
                DateUtil.GetMinutes(session.stime.ToJavaDate(), session.etime.ToJavaDate()));
        }

//        @BindingAdapter("sessionDetailTimeRange")
        public static string GetSessionDetailTimeRange(this Session session, Context context) 
        {
            return context.GetString(Resource.String.session_time_range,
                DateUtil.GetLongFormatDate(session.stime.ToJavaDate(), context),
                DateUtil.GetHourMinute(session.etime.ToJavaDate()),
                DateUtil.GetMinutes(session.stime.ToJavaDate(), session.etime.ToJavaDate()));
        }

//        @BindingAdapter("sessionDescription")
        public static string GetSessionDescription(this Session session) 
        {
            return session.description;
// TODO            Linkify.addLinks(textView, Linkify.ALL);
        }

//        @BindingAdapter("sessionFab")
        public static int GetSessionFabRippleColor(this Session session, Context context) 
        {
            return ContextCompat.GetColor(context, session.category.GetPaleColorResId());
        }

        public static bool GetSessionFabChecked(this Session session) 
        {
            return session.IsChecked;
        }

        public static int GetLanguageResId(this Session session) 
        {
            if ("en".Equals(session.language_id)) 
            {
                return Resource.String.lang_en;
            } 
            else if ("ja".Equals(session.language_id)) 
            {
                return Resource.String.lang_ja;
            } 
            else 
            {
                return Resource.String.lang_en;
            }
        }
    }
}

