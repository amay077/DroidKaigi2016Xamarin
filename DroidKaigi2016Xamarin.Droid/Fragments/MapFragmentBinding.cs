using System;
using Android.Support.V4.App;
using Android.Gms.Maps.Model;
using Android.Views;
using DroidKaigi2016Xamarin.Droid.Widgets;
using Android.Widget;
using io.github.droidkaigi.confsched.widget;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    class MapFragmentBinding
    {
        public View Root { get; }

        public readonly MapSearchView mapSearchView;
        public readonly FrameLayout loadingView;

        public static MapFragmentBinding Inflate(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            return new MapFragmentBinding(inflater, root, attachToRoot);
        }

        public MapFragmentBinding(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            Root = inflater.Inflate(Resource.Layout.fragment_map, root, attachToRoot);

            mapSearchView = Root.FindViewById<MapSearchView>(Resource.Id.map_search_view);
            loadingView = Root.FindViewById<FrameLayout>(Resource.Id.loading_view);
        }            
    }
}

