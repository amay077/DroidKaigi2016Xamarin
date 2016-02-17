using System;
using Android.App;
using Android.Content;
using DroidKaigi2016Xamarin.Core.Models;
using Stiletto;
using Fragment = Android.Support.V4.App.Fragment;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    [Singleton]
    public class ActivityNavigator
    {
        public void ShowSessionDetail(Activity activity, Session session, int requestCode) 
        {
            SessionDetailActivity.StartForResult(activity, session, requestCode);
        }

        public void ShowMain(Activity activity) 
        {
//            MainActivity.Start(activity);
        }

        public void ShowWebView(Context context, string url, string title) 
        {
//            WebViewActivity.start(context, url, title);
        }

        public void ShowSearch(Fragment fragment, int requestCode)         
        {
            SearchActivity.Start(fragment, requestCode);
        }

        public void ShowFeedback(Context context) 
        {
            // TODO
        }
    }
}

