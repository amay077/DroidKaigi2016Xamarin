using System;
using DroidKaigi2016Xamarin.Droid.Fragments;
using Stiletto;
using Android.Support.V4.App;

namespace DroidKaigi2016Xamarin.Droid.DIs
{
    public class FragmentComponent
    {
        private object[] modules;

        public FragmentComponent(object[] modules)
        {
            this.modules = modules;
        }

        public void Inject(SessionsFragment fragment)
        {
            var container = Container.Create(modules);
            container.Inject(fragment);
        }

        public void Inject(SessionsTabFragment fragment)
        {
            var container = Container.Create(modules);
            container.Inject(fragment);
        }

        public void Inject(SessionDetailFragment fragment)
        {
            var container = Container.Create(modules);
            container.Inject(fragment);
        }
    }
}

