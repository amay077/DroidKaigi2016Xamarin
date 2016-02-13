using System;
using Android.Views;
using Android.Support.V7.Widget;
using DroidKaigi2016Xamarin.Droid;

namespace io.github.droidkaigi.confsched.widget
{
    public class SearchToolbarBinding
    {
        public View Root  { get; }

        public readonly AppCompatEditText editSearch;
        public readonly Toolbar toolbar;

        public static SearchToolbarBinding Inflate(LayoutInflater inflater, int layoutId, SearchToolbar view, bool attachToRoot)
        {
            return new SearchToolbarBinding(inflater, layoutId, view, attachToRoot);
        }

        private SearchToolbarBinding(LayoutInflater inflater, int layoutId, SearchToolbar view, bool attachToRoot)
        {
            Root = inflater.Inflate(layoutId, view, attachToRoot);
            toolbar = Root.FindViewById<Toolbar>(Resource.Id.toolbar);
            editSearch = Root.FindViewById<AppCompatEditText>(Resource.Id.edit_search);

//            coverGithub = Root.FindViewById<View>(Resource.Id.cover_github);
        }
    }
}

