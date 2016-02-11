using System;
using Android.App;
using Android.Content;
using DroidKaigi2016Xamarin.Core.Models;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    public class ActivityNavigator
    {
        static readonly ActivityNavigator instance = new ActivityNavigator();
        static public ActivityNavigator Instance
        {
            get { return instance; }
        }

        public void ShowSessionDetail(Activity activity, Session session, int requestCode) {
//            SessionDetailActivity.StartForResult(activity, session, requestCode);
        }

        public void showMain(Activity activity) {
//            MainActivity.Start(activity);
        }

        public void showWebView(Context context, string url, string title) {
//            WebViewActivity.start(context, url, title);
        }

        public void showSearch(Activity activity) {
//            SearchActivity.start(activity);
        }

        public void showFeedback(Context context) {
            // TODO
        }
    }
}

