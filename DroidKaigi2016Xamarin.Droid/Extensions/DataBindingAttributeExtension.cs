using System;
using DroidKaigi2016Xamarin.Core.Models;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Android.Text;
using Square.Picasso;
using DroidKaigi2016Xamarin.Droid.Widgets.Transformations;
using Android.Text.Util;
using Android.Graphics;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class DataBindingAttributeExtension
    {
        //@BindingAdapter({"speakerImageUrl", "speakerImageSize"})
        public static void SetSpeakerImageUrlWithSize(this ImageView imageView, string imageUrl, float sizeInDimen) 
        {
            if (TextUtils.IsEmpty(imageUrl)) 
            {
                imageView.SetImageDrawable(ContextCompat.GetDrawable(imageView.Context, Resource.Drawable.ic_speaker_placeholder));
            } 
            else 
            {
                var size = (int)Math.Round(sizeInDimen);
                imageView.SetBackgroundDrawable(ContextCompat.GetDrawable(imageView.Context, Resource.Drawable.circle_border_grey200));
                Picasso.With(imageView.Context)
                    .Load(imageUrl)
                    .Resize(size, size)
                    .Placeholder(Resource.Drawable.ic_speaker_placeholder)
                    .Error(Resource.Drawable.ic_speaker_placeholder)
                    .Transform(new CropCircleTransformation())
                    .Into(imageView);
            }
        }

        //@BindingAdapter("coverFadeBackground")
        public static void SetCoverFadeBackground(this View view, Category category) 
        {
            view.SetBackgroundResource(category.GetPaleColorResId());
        }

        // @BindingAdapter("categoryVividColor")
        public static void SetCategoryVividColor(this CollapsingToolbarLayout view, Category category) 
        {
            view.SetContentScrimColor(ContextCompat.GetColor(view.Context, category.GetVividColorResId()));
        }

        // @BindingAdapter("categoryVividColor")
        public static void SetCategoryVividColor(this TextView view, Category category) 
        {
            view.SetTextColor(new Color(ContextCompat.GetColor(view.Context, category.GetVividColorResId())));
        }

//        @BindingAdapter("sessionTimeRange")
//        public static void setSessionTimeRange(TextView textView, @NonNull Session session) {
//            String timeRange = textView.getContext().getString(R.string.session_time_range,
//                DateUtil.getHourMinute(session.stime),
//                DateUtil.getHourMinute(session.etime),
//                DateUtil.getMinutes(session.stime, session.etime));
//            textView.setText(timeRange);
//        }
//
//        @BindingAdapter("sessionDetailTimeRange")
//        public static void setSessionDetailTimeRange(TextView textView, @NonNull Session session) {
//            String timeRange = textView.getContext().getString(R.string.session_time_range,
//                DateUtil.getLongFormatDate(session.stime, textView.getContext()),
//                DateUtil.getHourMinute(session.etime),
//                DateUtil.getMinutes(session.stime, session.etime));
//            textView.setText(timeRange);
//        }
//
        //@BindingAdapter("sessionDescription")
        public static void SetSessionDescription(this TextView textView, Session session) 
        {
            textView.Text = session.description;
            Linkify.AddLinks(textView, MatchOptions.All);
        }

        //@BindingAdapter("sessionFab")
        public static void SetSessionFab(this FloatingActionButton fab, Session session) 
        {
            fab.SetRippleColor(new Color(ContextCompat.GetColor(fab.Context, session.category.GetPaleColorResId())));
            fab.Selected = session.IsChecked;
        }

    }
}

