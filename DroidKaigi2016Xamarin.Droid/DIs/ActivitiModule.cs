using System;
using Stiletto;
using Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.Views;
using System.Reactive.Disposables;
using DroidKaigi2016Xamarin.Droid.Activities;

namespace DroidKaigi2016Xamarin.Droid.DIs
{
    [Module(Injects = new [] {
        typeof(MainActivity),
        typeof(SessionDetailActivity),
        typeof(SearchActivity),
        typeof(SearchedSessionsActivity),
    })]
    public class ActivityModule 
    {
        private readonly AppCompatActivity activity;

        public ActivityModule(AppCompatActivity activity) 
        {
            this.activity = activity;
        }

        [Provides]
        public Activity GetActivity()
        {
            return activity;
        }

        [Provides]
        public Context GetContext()
        {
            return activity;
        }

        [Provides]
        public LayoutInflater GetLayoutInflater()
        {
            return activity.LayoutInflater;
        }
    }
}

