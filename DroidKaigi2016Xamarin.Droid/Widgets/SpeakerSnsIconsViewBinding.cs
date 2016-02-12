using System;
using Android.Views;
using DroidKaigi2016Xamarin.Droid;

namespace io.github.droidkaigi.confsched.widget
{
    public class SpeakerSnsIconsViewBinding
    {
        public View Root  { get; }

        public readonly View coverTwitter;
        public readonly View coverGithub;

        public static SpeakerSnsIconsViewBinding Inflate(LayoutInflater inflater, int layoutId, SpeakerSnsIconsView view, bool attachToRoot)
        {
            return new SpeakerSnsIconsViewBinding(inflater, layoutId, view, attachToRoot);
        }

        private SpeakerSnsIconsViewBinding(LayoutInflater inflater, int layoutId, SpeakerSnsIconsView view, bool attachToRoot)
        {
            Root = inflater.Inflate(layoutId, view, attachToRoot);
            coverTwitter = Root.FindViewById<View>(Resource.Id.cover_twitter);
            coverGithub = Root.FindViewById<View>(Resource.Id.cover_github);

        }
    }
}

