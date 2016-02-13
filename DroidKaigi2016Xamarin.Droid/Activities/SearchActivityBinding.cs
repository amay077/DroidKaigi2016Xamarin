using System;
using Android.App;
using Android.Views;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    public class SearchActivityBinding
    {
        public object searchPlacesAndCategoriesView
        {
            get;
            set;
        }

        public ViewGroup Root { get; }

        public static SearchActivityBinding SetContentView(Activity activity, int layoutId)
        {
            return new SearchActivityBinding(activity, layoutId);
        }

        private SearchActivityBinding(Activity activity, int layoutId)
        {
            activity.SetContentView(layoutId);
            toolbar = activity.FindViewById<Toolbar>(Resource.Id.toolbar);
//            drawer  = activity.FindViewById<DrawerLayout>(Resource.Id.drawer);
//            navView = activity.FindViewById<NavigationView>(Resource.Id.nav_view);

            var decorView = activity.Window.DecorView;
            Root = (ViewGroup) decorView.FindViewById(global::Android.Resource.Id.Content);
        }
    }
}

