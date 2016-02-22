using System;
using Android.Widget;
using Android.Views;

namespace DroidKaigi2016Xamarin.Droid.Widgets
{
	class SponsorImageViewBinding
	{
        public View Root  { get; }

        public readonly ViewGroup rootView;
        public readonly ImageView imgLogo;

        public static SponsorImageViewBinding Inflate(LayoutInflater inflater, int layoutId, SponsorImageView view, bool attachToRoot)
        {
            return new SponsorImageViewBinding(inflater, layoutId, view, attachToRoot);
        }

        private SponsorImageViewBinding(LayoutInflater inflater, int layoutId, SponsorImageView view, bool attachToRoot)
        {
            Root = inflater.Inflate(layoutId, view, attachToRoot);
            rootView = Root.FindViewById<ViewGroup>(Resource.Id.root_view);
            imgLogo = Root.FindViewById<ImageView>(Resource.Id.img_logo);
        }
    }

}

