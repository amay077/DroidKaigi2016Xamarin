using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using DroidKaigi2016Xamarin.Core.Models;
using Square.Picasso;

namespace DroidKaigi2016Xamarin.Droid.Widgets
{
    public class SponsorImageView : FrameLayout 
    {
        private SponsorImageViewBinding binding;

        public SponsorImageView(Context context) : this(context, null)
        {
        }

        public SponsorImageView(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public SponsorImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            binding = SponsorImageViewBinding.Inflate(LayoutInflater.From(context), Resource.Layout.view_sponsor_image, this, true);
        }

        public void BindData(Sponsor sponsor, EventHandler listener) 
        {
            Picasso.With(Context)
                .Load(sponsor.ImageUrl)
                .Placeholder(Resource.Color.grey200)
                .Into(binding.imgLogo);

            binding.rootView.Click += listener;
        }

    }
}

