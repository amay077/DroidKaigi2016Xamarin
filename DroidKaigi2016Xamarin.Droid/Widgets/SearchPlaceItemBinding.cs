using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using DroidKaigi2016Xamarin.Droid;
using System.Collections.Generic;
using DroidKaigi2016Xamarin.Core.Models;
using Android.Support.V7.Widget;
using DroidKaigi2016Xamarin.Droid.Widgets;

namespace io.github.droidkaigi.confsched.widget
{
	public class SearchPlaceItemBinding
	{
        public View Root { get; }

        public SearchPlaceItemBinding(View view)
        {
            Root = view;
        }

        public void SetPlace(Place place)
        {
            Root.FindViewById<TextView>(Resource.Id.txt_place).Text = place.Name;
        }
	}


}

