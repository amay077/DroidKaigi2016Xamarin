using System;
using Android.Support.V4.App;
using Android.Views;
using Apmem;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SponsorsFragmentBinding
    {
        public View Root { get; }

        public readonly FlowLayout platinumContainer;
        public readonly FlowLayout videoContainer;
        public readonly FlowLayout foodsContainer;
        public readonly FlowLayout normalContainer;

        public static SponsorsFragmentBinding Inflate(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            return new SponsorsFragmentBinding(inflater, root, attachToRoot);
        }

        public SponsorsFragmentBinding(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            Root = inflater.Inflate(Resource.Layout.fragment_sponsors, root, attachToRoot);
            platinumContainer = Root.FindViewById<FlowLayout>(Resource.Id.platinum_container);
            videoContainer = Root.FindViewById<FlowLayout>(Resource.Id.video_container);
            foodsContainer = Root.FindViewById<FlowLayout>(Resource.Id.foods_container);
            normalContainer = Root.FindViewById<FlowLayout>(Resource.Id.normal_container);
        }    
    }
}

