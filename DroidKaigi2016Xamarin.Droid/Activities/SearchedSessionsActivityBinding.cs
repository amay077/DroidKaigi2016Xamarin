using System;
using Android.App;
using Android.Views;
using Android.Support.V7.Widget;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    public class SearchedSessionsActivityBinding
    {
        public ViewGroup Root { get; }

        public readonly Toolbar toolbar;

        public static SearchedSessionsActivityBinding SetContentView(Activity activity, int layoutId)
        {
            return new SearchedSessionsActivityBinding(activity, layoutId);
        }

        private SearchedSessionsActivityBinding(Activity activity, int layoutId)
        {
            activity.SetContentView(layoutId);
            toolbar = activity.FindViewById<Toolbar>(Resource.Id.toolbar);

            var decorView = activity.Window.DecorView;
            Root = (ViewGroup) decorView.FindViewById(global::Android.Resource.Id.Content);
        }
    }
}

