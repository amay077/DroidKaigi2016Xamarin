using System;
using Android.Support.V7.Widget;
using Android.Widget;
using io.github.droidkaigi.confsched.widget;
using Android.Views;
using DroidKaigi2016Xamarin.Core.Models;
using DroidKaigi2016Xamarin.Droid.Utils;
using DroidKaigi2016Xamarin.Droid.Extensions;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    class SessionItemViewBinder : RecyclerView.ViewHolder 
    {
        public readonly TextView txtTitle;

        public readonly TextView txtStime;
        public readonly TextView txtTime;
        public readonly TextView txtPlace;
        public readonly CategoryView txtCategory;
        public readonly TextView txtLanguage;
        public readonly ImageView imgSpeaker;
        public readonly TextView txtSpeakerName;
        public readonly Com.Like.LikeButton btnStar;
        public readonly View cardView;

        public static SessionItemViewBinder NewInstance(ViewGroup parent)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_session, parent, false);
            return new SessionItemViewBinder(view);
        }

        private SessionItemViewBinder(View view) : base(view)
        {
            txtTitle = view.FindViewById<TextView>(Resource.Id.txt_title);
            txtStime = view.FindViewById<TextView>(Resource.Id.txt_stime);
            txtTime = view.FindViewById<TextView>(Resource.Id.txt_time);
            txtPlace = view.FindViewById<TextView>(Resource.Id.txt_place);
            txtCategory = view.FindViewById<CategoryView>(Resource.Id.txt_category);
            txtLanguage = view.FindViewById<TextView>(Resource.Id.txt_language);
            imgSpeaker = view.FindViewById<ImageView>(Resource.Id.img_speaker);
            txtSpeakerName = view.FindViewById<TextView>(Resource.Id.txt_speaker_name);
            btnStar = view.FindViewById<Com.Like.LikeButton>(Resource.Id.btn_star);
            cardView = view.FindViewById<View>(Resource.Id.card_view);
        }

        public void SetSession(Session session)
        {
            txtTitle.Text = session.title;
            txtStime.Text = DateUtil.GetHourMinute(session.stime.ToJavaDate());
            txtTime.Text  = session.GetSessionTimeRange(ItemView.Context);
            txtPlace.Text = session.place?.name ?? string.Empty;
            txtCategory.Category = session.category;
            txtLanguage.SetText(session.GetLanguageResId());
            imgSpeaker.SetSpeakerImageUrlWithSize(session.speaker?.image_url ?? string.Empty, 
                ItemView.Context.Resources.GetDimension(Resource.Dimension.user_image_small));
            txtSpeakerName.Text = session.speaker?.name ?? string.Empty;
            btnStar.SetLiked(new Java.Lang.Boolean(session.IsChecked));
        }
    }
}

