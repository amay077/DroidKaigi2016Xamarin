using System;
using Android.Views;
using Android.Support.V7.Widget;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public sealed class SessionsTabFragmentBinding
    {
        public View Root { get; }

        public readonly RecyclerView recyclerView;

        public static SessionsTabFragmentBinding Inflate(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            return new SessionsTabFragmentBinding(inflater, root, attachToRoot);
        }

        public SessionsTabFragmentBinding(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            Root = inflater.Inflate(Resource.Layout.fragment_sessions_tab, root, attachToRoot);
            recyclerView = Root.FindViewById<RecyclerView>(Resource.Id.recycler_view);
//            tabLayout = Root.FindViewById<TabLayout>(Resource.Id.tab_layout);
//            emptyViewButton = Root.FindViewById<AppCompatButton>(Resource.Id.empty_view_button);
//            emptyView = Root.FindViewById<View>(Resource.Id.empty_view);
        }

    }
}

