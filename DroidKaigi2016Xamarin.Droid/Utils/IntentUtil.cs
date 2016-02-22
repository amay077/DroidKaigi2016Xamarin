using System;
using Android.Content;

namespace DroidKaigi2016Xamarin.Droid.Utils
{
    public static class IntentUtil 
    {
        public static Intent ToBrowser(string url) 
        {
            var uri = Android.Net.Uri.Parse(url);
            return new Intent(Intent.ActionView, uri);
        }
    }
}

