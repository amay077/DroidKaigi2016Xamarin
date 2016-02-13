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
	public class SearchGroupsViewBinder : RecyclerView.ViewHolder
	{
		public SearchGroupsViewBinder(Context context, ViewGroup parent, int layoutId)
		{
            var view = LayoutInflater.From(context).Inflate(layoutId, parent, false);
		}
	}

}

