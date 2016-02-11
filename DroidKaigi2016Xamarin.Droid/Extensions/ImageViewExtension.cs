using System;
using Android.Widget;
using Android.Text;
using Android.Support.V4.Content;
using Square.Picasso;
using DroidKaigi2016Xamarin.Droid.Widgets.Transformations;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class ImageViewExtension
    {
//        @BindingAdapter({"speakerImageUrl", "speakerImageSize"})
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
        }    }
}

