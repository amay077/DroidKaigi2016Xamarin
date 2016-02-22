using System;
using Android.Widget;
using Android.Views.Animations;
using Android.Content;
using Android.Util;
using Android.Views;
using System.Collections.Generic;
using DroidKaigi2016Xamarin.Core.Models;
using System.Linq;

namespace DroidKaigi2016Xamarin.Droid.Widgets
{
	class MapSearchViewItemBinding
	{
        public View Root  { get; }

        public readonly ImageView imgMarker;
        public readonly TextView txtName;
        public readonly TextView txtBuilding;
        public readonly ViewGroup rootView;

        public static MapSearchViewItemBinding Inflate(LayoutInflater inflater, int layoutId, MapSearchViewItem view, bool attachToRoot)
        {
            return new MapSearchViewItemBinding(inflater, layoutId, view, attachToRoot);
        }

        private MapSearchViewItemBinding(LayoutInflater inflater, int layoutId, MapSearchViewItem view, bool attachToRoot)
        {
            Root = inflater.Inflate(layoutId, view, attachToRoot);
            imgMarker = Root.FindViewById<ImageView>(Resource.Id.img_marker);
            txtName = Root.FindViewById<TextView>(Resource.Id.txt_name);
            txtBuilding = Root.FindViewById<TextView>(Resource.Id.txt_building);
            rootView = Root.FindViewById<ViewGroup>(Resource.Id.root_view);
        }
	}

}

