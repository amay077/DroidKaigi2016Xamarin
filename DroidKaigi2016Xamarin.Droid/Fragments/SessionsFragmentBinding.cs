using System;
using Android.Views;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SessionsFragmentBinding
    {
        public View Root { get; }

        public readonly ViewPager viewPager;
        public readonly TabLayout tabLayout;
        public readonly AppCompatButton emptyViewButton;
        public readonly View emptyView;

        public static SessionsFragmentBinding Inflate(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            return new SessionsFragmentBinding(inflater, root, attachToRoot);
        }

        public SessionsFragmentBinding(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            Root = inflater.Inflate(Resource.Layout.fragment_sessions, root, attachToRoot);
            viewPager = Root.FindViewById<ViewPager>(Resource.Id.view_pager);
            tabLayout = Root.FindViewById<TabLayout>(Resource.Id.tab_layout);
            emptyViewButton = Root.FindViewById<AppCompatButton>(Resource.Id.empty_view_button);
            emptyView = Root.FindViewById<View>(Resource.Id.empty_view);
        }
    }
}

