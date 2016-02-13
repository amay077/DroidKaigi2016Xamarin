using System;
using Android.Content;
using Android.Support.V4.Text;
using Android.Support.V4.View;

namespace DroidKaigi2016Xamarin.Droid.Utils
{
    public static class LocaleUtil 
    {
        private static readonly string RTL_MARK = "\u200F";

        public static bool ShouldRtl(Context context) 
        {
            return TextUtilsCompat.GetLayoutDirectionFromLocale(AppUtil.GetCurrentLocale(context)) == ViewCompat.LayoutDirectionRtl;
        }

        public static String GetRtlConsideredText(string text, Context context) {
            if (ShouldRtl(context)) 
            {
                return RTL_MARK + text;
            } 
            else 
            {
                return text;
            }
        }

    }
}

