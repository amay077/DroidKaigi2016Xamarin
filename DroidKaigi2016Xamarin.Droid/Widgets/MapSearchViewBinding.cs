using System;
using Android.Widget;
using Android.Views.Animations;
using Android.Views;
using DroidKaigi2016Xamarin.Droid;

namespace io.github.droidkaigi.confsched.widget
{
    class MapSearchViewBinding
    {
        public View Root  { get; }

        public readonly ViewGroup mapListContainer;

        public static MapSearchViewBinding Inflate(LayoutInflater inflater, int layoutId, MapSearchView view, bool attachToRoot)
        {
            return new MapSearchViewBinding(inflater, layoutId, view, attachToRoot);
        }

        private MapSearchViewBinding(LayoutInflater inflater, int layoutId, MapSearchView view, bool attachToRoot)
        {
            Root = inflater.Inflate(layoutId, view, attachToRoot);
            mapListContainer = Root.FindViewById<ViewGroup>(Resource.Id.map_list_container);

        }
    }
}

