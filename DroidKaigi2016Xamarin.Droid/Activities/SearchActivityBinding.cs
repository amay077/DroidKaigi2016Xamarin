using System;
using Android.App;
using Android.Views;
using io.github.droidkaigi.confsched.widget;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    public class SearchActivityBinding
    {
        public ViewGroup Root { get; }

        public readonly SearchPlacesAndCategoriesView searchPlacesAndCategoriesView;
        public readonly SearchToolbar searchToolbar;
        public readonly DrawerLayout drawer;
        public readonly NavigationView navView;
        public readonly RecyclerView recyclerView;

        public static SearchActivityBinding SetContentView(Activity activity, int layoutId)
        {
            return new SearchActivityBinding(activity, layoutId);
        }

        private SearchActivityBinding(Activity activity, int layoutId)
        {
            activity.SetContentView(layoutId);
            searchPlacesAndCategoriesView = activity.FindViewById<SearchPlacesAndCategoriesView>(
                Resource.Id.search_places_and_categories_view);
            searchToolbar = activity.FindViewById<SearchToolbar>(Resource.Id.search_toolbar);
            
            drawer  = activity.FindViewById<DrawerLayout>(Resource.Id.drawer);
            navView = activity.FindViewById<NavigationView>(Resource.Id.nav_view);
            recyclerView = activity.FindViewById<RecyclerView>(Resource.Id.recycler_view);

            var decorView = activity.Window.DecorView;
            Root = (ViewGroup) decorView.FindViewById(global::Android.Resource.Id.Content);
        }
    }
}

