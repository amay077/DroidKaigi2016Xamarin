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
        public readonly object binding;

        public static SearchGroupsViewBinder NewInstance(Context context, ViewGroup parent, int viewType)
		{
            View view;
            object binding;
            switch (viewType) 
            {
                case (int)SearchType.CATEGORY:
                    view = LayoutInflater.From(context).Inflate(Resource.Layout.item_search_category, parent, false);
                    binding = new SearchCategoryItemBinding(view);
                    break;
                case (int)SearchType.PLACE:
                    view = LayoutInflater.From(context).Inflate(Resource.Layout.item_search_place, parent, false);
                    binding = new SearchPlaceItemBinding(view);
                    break;
                case (int)SearchType.TITLE:
                    view = LayoutInflater.From(context).Inflate(Resource.Layout.item_search_title, parent, false);
                    binding = new SearchTitleItemBinding(view);
                    break;
                default:
                    throw new InvalidOperationException("ViewType is invalid: " + viewType.ToString());
            }

            return new SearchGroupsViewBinder(view, binding);
		}

        private SearchGroupsViewBinder(View view, object binding) : base(view)
        {
            this.binding = binding;
        }
	}

}

