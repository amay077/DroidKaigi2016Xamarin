using System;
using Android.Content;
using Android.Preferences;

namespace DroidKaigi2016Xamarin.Droid.Utils
{
    public static class PrefUtil
    {
        private static readonly string KEY_CURRENT_LANGUAGE_ID = "current_language_id";

        private static ISharedPreferences pref;

        public static ISharedPreferences GetPref(Context context) 
        {
            if (pref == null) 
            {
                pref = PreferenceManager.GetDefaultSharedPreferences(context);
            }
            return pref;
        }

        public static void Put(Context context, string name, bool value) 
        {
            var edit = GetPref(context).Edit();
            edit.PutBoolean(name, value);
            edit.Apply();
        }

        public static void Put(Context context, string name, string value) 
        {
            var edit = GetPref(context).Edit();
            edit.PutString(name, value);
            edit.Apply();
        }

        public static bool Contains(Context context, string name) 
        {
            return GetPref(context).Contains(name);
        }

        public static void Remove(Context context, string name) 
        {
            var edit = GetPref(context).Edit();
            edit.Remove(name);
            edit.Apply();
        }

        public static string Get(Context context, string name, string defaultValue) 
        {
            return GetPref(context).GetString(name, defaultValue);
        }    
    }
}

