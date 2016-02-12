using System;
using Android.Views;
using Android.Support.Design.Widget;
using DroidKaigi2016Xamarin.Core.Models;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using DroidKaigi2016Xamarin.Droid.Utils;
using DroidKaigi2016Xamarin.Droid.Extensions;
using io.github.droidkaigi.confsched.widget;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SessionDetailFragmentBinding
    {
        public View Root { get; }

        public readonly FloatingActionButton fab;
        public readonly TextView txtFeedback;
        public readonly Toolbar toolbar;
        public readonly AppBarLayout appbar;
        public readonly CollapsingToolbarLayout collapsingToolbar;
        public readonly TextView txtSessionTimeRange;
        public readonly ImageView imgCover;
        public readonly TextView txtPlace;

        public readonly CategoryView txtCategory;

        public readonly TextView txtLanguage;

        TextView txtSpeakerTitle;
        ImageView imgSpeaker;

        TextView txtSpeakerName;

        TextView txtDescriptionDetail;

        TextView txtDescriptionTitle;

        SpeakerSnsIconsView speakerSnsIcons;

        public static SessionDetailFragmentBinding Inflate(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            return new SessionDetailFragmentBinding(inflater, root, attachToRoot);
        }


        private SessionDetailFragmentBinding(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            Root = inflater.Inflate(Resource.Layout.fragment_session_detail, root, attachToRoot);


            fab = Root.FindViewById<FloatingActionButton>(Resource.Id.fab);
            toolbar = Root.FindViewById<Toolbar>(Resource.Id.toolbar);
            appbar = Root.FindViewById<AppBarLayout>(Resource.Id.app_bar);
            collapsingToolbar = Root.FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            txtSessionTimeRange = Root.FindViewById<TextView>(Resource.Id.txt_session_time_range);
            imgCover = Root.FindViewById<ImageView>(Resource.Id.img_cover);
            txtPlace = Root.FindViewById<TextView>(Resource.Id.txt_place);
            txtCategory = Root.FindViewById<CategoryView>(Resource.Id.txt_category);
            txtLanguage = Root.FindViewById<TextView>(Resource.Id.txt_language);
            txtSpeakerTitle = Root.FindViewById<TextView>(Resource.Id.txt_speaker_title);
            imgSpeaker = Root.FindViewById<ImageView>(Resource.Id.img_speaker);
            txtSpeakerName = Root.FindViewById<TextView>(Resource.Id.txt_speaker_name);
            txtDescriptionTitle = Root.FindViewById<TextView>(Resource.Id.txt_description_title);
            txtDescriptionDetail = Root.FindViewById<TextView>(Resource.Id.txt_description_detail);
            txtFeedback = Root.FindViewById<TextView>(Resource.Id.txt_feedback);
            fab = Root.FindViewById<FloatingActionButton>(Resource.Id.fab);
            speakerSnsIcons = Root.FindViewById<SpeakerSnsIconsView>(Resource.Id.speaker_sns_icons);


        }

        public void SetSession(Session session)
        {
            collapsingToolbar.Title = session.title;
            collapsingToolbar.SetCategoryVividColor(session?.category);
            txtSessionTimeRange.Text = session.GetSessionTimeRange(Root.Context);
            imgCover.SetCoverFadeBackground(session?.category);
            txtPlace.Text = session?.place?.name ?? string.Empty;
            txtCategory.Category = session.category;
            txtLanguage.SetText(session.GetLanguageResId());
            txtSpeakerTitle.SetCategoryVividColor(session?.category);
            imgSpeaker.SetSpeakerImageUrlWithSize(session.speaker?.image_url ?? string.Empty, 
                Root.Context.Resources.GetDimension(Resource.Dimension.user_image_small));
            txtSpeakerName.Text = session?.speaker?.name ?? string.Empty;
            txtDescriptionTitle.SetCategoryVividColor(session?.category);
            txtDescriptionDetail.SetSessionDescription(session);
            txtFeedback.SetCategoryVividColor(session?.category);
            fab.SetSessionFab(session);
            SpeakerSnsIconsView.SetSpeakerSnsIcons(speakerSnsIcons, session?.speaker);
        }
    }
}

