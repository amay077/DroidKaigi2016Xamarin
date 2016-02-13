using System;
using Android.Views;
using Android.Support.V7.Widget;
using DroidKaigi2016Xamarin.Droid;

namespace io.github.droidkaigi.confsched.widget
{
    public class SearchPlacesAndCategoriesViewBinding
    {
        public View Root  { get; }

        public readonly RecyclerView recyclerView;
           
        public static SearchPlacesAndCategoriesViewBinding Inflate(LayoutInflater inflater, int layoutId, SearchPlacesAndCategoriesView view, bool attachToRoot)
        {
            return new SearchPlacesAndCategoriesViewBinding(inflater, layoutId, view, attachToRoot);
        }

        public SearchPlacesAndCategoriesViewBinding(LayoutInflater inflater, int layoutId, SearchPlacesAndCategoriesView view, bool attachToRoot)
        {
            Root = inflater.Inflate(layoutId, view, attachToRoot);
            recyclerView = Root.FindViewById<RecyclerView>(Resource.Id.recycler_view);

        }
    }
}

