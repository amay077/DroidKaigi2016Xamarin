using System;
using Stiletto;
using DroidKaigi2016Xamarin.Droid.Fragments;
using Android.Support.V4.App;

namespace DroidKaigi2016Xamarin.Droid.DIs
{
    [Module(Injects = new [] {
        typeof(SessionsFragment),
        typeof(SessionsTabFragment),
        typeof(SessionDetailFragment),
    })]
    public class FragmentModule
    {
        private readonly Fragment fragment;

        public FragmentModule(Fragment fragment)
        {
            this.fragment = fragment;
        }

        [Provides]
        public FragmentManager ProvideFragmentManager() 
        {
            return fragment.FragmentManager;
        }
    }
}

