using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using DroidKaigi2016Xamarin.Core.Models;
using DroidKaigi2016Xamarin.Droid.Extensions;
using DroidKaigi2016Xamarin.Droid.Utils;
using Android.App;
using DroidKaigi2016Xamarin.Droid;

namespace io.github.droidkaigi.confsched.widget
{
    public class SpeakerSnsIconsView : RelativeLayout 
    {
        private SpeakerSnsIconsViewBinding binding;

        public SpeakerSnsIconsView(Context context) : this(context, null)
        {
        }

        public SpeakerSnsIconsView(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public SpeakerSnsIconsView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            binding = SpeakerSnsIconsViewBinding.Inflate(LayoutInflater.From(context), Resource.Layout.view_speaker_sns_icons, this, true);
        }

//        @BindingAdapter("speakerSnsIcons")
        public static void SetSpeakerSnsIcons(SpeakerSnsIconsView view, Speaker speaker) 
        {
            var binding = view.binding;

            if (string.IsNullOrEmpty(speaker.twitter_name) && string.IsNullOrEmpty(speaker.github_name)) 
            {
                binding.Root.Visibility = ViewStates.Gone;
                return;
            }

            if (string.IsNullOrEmpty(speaker.twitter_name)) 
            {
                binding.coverTwitter.Visibility = ViewStates.Gone;
            }
            else 
            {
                binding.coverTwitter.Visibility = ViewStates.Visible;
                binding.coverTwitter.SetOnClickAction(v =>
                    AppUtil.ShowWebPage(view.Context as Activity, AppUtil.GetTwitterUrl(speaker.twitter_name)));
            }

            if (string.IsNullOrEmpty(speaker.github_name)) 
            {
                binding.coverGithub.Visibility = ViewStates.Gone;
            }
            else 
            {
                binding.coverGithub.Visibility = ViewStates.Visible;
                binding.coverGithub.SetOnClickAction(v =>
                    AppUtil.ShowWebPage(view.Context as Activity, AppUtil.GetGitHubUrl(speaker.github_name)));
            }
        }

    }
}

