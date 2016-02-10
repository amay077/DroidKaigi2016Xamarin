using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    public class MainActivityBinding
    {
        public readonly Toolbar toolbar;
        public readonly DrawerLayout drawer;
        public readonly NavigationView navView;

        public static MainActivityBinding SetContentView(Activity activity, int layoutId)
        {
            return new MainActivityBinding(activity, layoutId);
        }

        public MainActivityBinding(Activity activity, int layoutId)
        {
            toolbar = activity.FindViewById<Toolbar>(Resource.Id.toolbar);
            drawer  = activity.FindViewById<DrawerLayout>(Resource.Id.drawer);
            navView = activity.FindViewById<NavigationView>(Resource.Id.nav_view);
        }
    }
}

