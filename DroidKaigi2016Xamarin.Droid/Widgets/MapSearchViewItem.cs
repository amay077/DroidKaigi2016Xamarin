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
    public class MapSearchViewItem : FrameLayout 
    {
        private MapSearchViewItemBinding binding;

        public MapSearchViewItem(Context context) : this(context, null)
        {
        }

        public MapSearchViewItem(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public MapSearchViewItem(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            binding = MapSearchViewItemBinding.Inflate(LayoutInflater.From(context), Resource.Layout.view_map_search_item, this, true);
        }

        public void BindData(PlaceMap placeMap, EventHandler listener) 
        {
            binding.imgMarker.SetImageResource(placeMap.markerRes);
            binding.txtName.SetText(placeMap.nameRes);
            binding.txtBuilding.SetText(placeMap.buildingNameRes);
            binding.rootView.Click += listener;
        }

    }
}

